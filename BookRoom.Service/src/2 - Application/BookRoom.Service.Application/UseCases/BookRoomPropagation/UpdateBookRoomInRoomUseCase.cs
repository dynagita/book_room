using AutoMapper;
using BookRoom.Service.Application.UseCases.RoomPropagation;
using BookRoom.Service.Domain.Contract.Notifications;
using BookRoom.Service.Domain.Contract.UseCases;
using BookRoom.Service.Domain.Repositories;
using BookRoom.Services.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace BookRoom.Service.Application.UseCases.BookRoomPropagation
{
    public class UpdateBookRoomInRoomUseCase : IUpdateBookRoomInRoomUseCase
    {
        private readonly IMapper _mapper;
        private readonly IRoomRepository _repository;
        private readonly ILogger<UpdateRoomBookRoomUseCase> _logger;

        public UpdateBookRoomInRoomUseCase(
            ILogger<UpdateRoomBookRoomUseCase> logger,
            IRoomRepository repository,
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
                var roomDb = await _repository.FindOneAsync(room.Room.Reference, cancellationToken);
                if (roomDb != null)
                {
                    var books = roomDb.Books?.ToList();
                    if (books != null && books.Any())
                    {
                        var index = books.FindIndex(x => x.Reference == request.Reference);
                        if (index >= 0)
                        {
                            var bookRoomEntity = _mapper.Map<BookRooms>(request);
                            books[index] = bookRoomEntity;
                            roomDb.Books = books;
                            await _repository.UpdateAsync(roomDb, cancellationToken);
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
