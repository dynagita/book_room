using AutoMapper;
using BookRoom.Service.Application.UseCases.UserPropagation;
using BookRoom.Service.Domain.Contract.Notifications;
using BookRoom.Service.Domain.Contract.UseCases;
using BookRoom.Service.Domain.Repositories;
using BookRoom.Services.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace BookRoom.Service.Application.UseCases.BookRoomPropagation
{
    public class PropagateBookRoomUseCase : IPropagateBookRoomUseCase
    {
        private readonly IBookRoomRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<PropagateUserUseCase> _logger;
        private readonly IUpdateBookRoomInRoomUseCase _propagateToRoom;
        private readonly IUpdateBookRoomInUserUseCase _propagateToUser;

        public PropagateBookRoomUseCase(
            ILogger<PropagateUserUseCase> logger,
            IMapper mapper,
            IBookRoomRepository repository,
            IUpdateBookRoomInRoomUseCase propagateToRoom,
            IUpdateBookRoomInUserUseCase propagateToUser)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
            _propagateToRoom = propagateToRoom;
            _propagateToUser = propagateToUser;
        }

        public async Task HandleAsync(PropagateBookRoomNotification request, CancellationToken cancellationToken)
        {
            try
            {
                var bookRoomDb = await _repository.FindOneAsync(request.Reference, cancellationToken);

                var bookRoomEvent = _mapper.Map<BookRooms>(request);
                if (bookRoomDb != null)
                {
                    bookRoomEvent.Id = bookRoomDb.Id;
                    await _repository.UpdateAsync(bookRoomEvent, cancellationToken);
                }
                else
                {
                    await _repository.InsertAsync(bookRoomEvent, cancellationToken);
                }

                bookRoomDb = await _repository.FindOneAsync(request.Reference, cancellationToken);

                var propagateBookRoom = _mapper.Map<PropagateBookRoomNotification>(bookRoomDb);

                await _propagateToRoom.HandleAsync(propagateBookRoom, cancellationToken);
                await _propagateToUser.HandleAsync(propagateBookRoom, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{UseCase} - {Method} - Exception Thrown", nameof(PropagateUserUseCase), nameof(HandleAsync));
                //Implement super resilience process here
            }
        }
    }
}
