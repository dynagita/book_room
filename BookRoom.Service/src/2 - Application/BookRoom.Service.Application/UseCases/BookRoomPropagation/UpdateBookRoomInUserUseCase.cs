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
        private readonly ILogger<UpdateRoomBookRoomUseCase> _logger;

        public UpdateBookRoomInUserUseCase(
            ILogger<UpdateRoomBookRoomUseCase> logger,
            IUserRepository repository,
            IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task HandleAsync(PropagateBookRoomNotification request, CancellationToken cancellationToken)
        {
            try
            {
                var room = _mapper.Map<BookRooms>(request);
                var userDb = await _repository.FindOneAsync(room.User.Reference, cancellationToken);
                if (userDb != null)
                {
                    var books = userDb.Books?.ToList();
                    if (books != null && books.Any())
                    {
                        var index = books.FindIndex(x => x.Reference == request.Reference);
                        if (index >= 0)
                        {
                            var bookRoomEntity = _mapper.Map<BookRooms>(request);
                            books[index] = bookRoomEntity;
                            userDb.Books = books;
                            await _repository.UpdateAsync(userDb, cancellationToken);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{UseCase} - {Method} - Exception Thrown", nameof(UpdateRoomBookRoomUseCase), nameof(HandleAsync));
                //Implement super resilience process here
            }
        }
    }
}
