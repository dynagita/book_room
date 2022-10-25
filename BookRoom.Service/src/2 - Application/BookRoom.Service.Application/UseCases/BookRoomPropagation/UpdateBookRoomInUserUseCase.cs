using AutoMapper;
using BookRoom.Service.Application.UseCases.RoomPropagation;
using BookRoom.Service.Domain.Contract.Notifications;
using BookRoom.Service.Domain.Contract.UseCases;
using BookRoom.Service.Domain.Repositories;
using BookRoom.Services.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace BookRoom.Service.Application.UseCases.BookRoomPropagation
{
    public class UpdateBookRoomInUserUseCase : IUpdateBookRoomInUserUseCase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _repository;
        private readonly ILogger<UpdateBookRoomInUserUseCase> _logger;

        public UpdateBookRoomInUserUseCase(
            ILogger<UpdateBookRoomInUserUseCase> logger,
            IUserRepository repository,
            IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task HandleAsync(BookRoomsNotification request, CancellationToken cancellationToken)
        {
            try
            {
                var room = _mapper.Map<BookRooms>(request);
                var userDb = await _repository.FindOneAsync(room.User.Id, cancellationToken);
                if (userDb != null)
                {
                    var bookRoomEntity = _mapper.Map<BookRooms>(request);
                    var books = userDb.Books?.ToList() ?? new List<BookRooms>();
                    if (books.Any())
                    {
                        var index = books.FindIndex(x => x.Id == request.Id);
                        if (index >= 0)
                        {                            
                            books[index] = bookRoomEntity;                            
                        }
                        else
                        {
                            books.Add(bookRoomEntity);
                        }
                    }
                    else
                    {
                        books.Add(bookRoomEntity);
                    }
                    userDb.Books = books;
                    await _repository.UpdateAsync(userDb, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{UseCase} - {Method} - Exception Thrown", nameof(UpdateBookRoomInUserUseCase), nameof(HandleAsync));
                //Implement super resilience process here
            }
        }
    }
}
