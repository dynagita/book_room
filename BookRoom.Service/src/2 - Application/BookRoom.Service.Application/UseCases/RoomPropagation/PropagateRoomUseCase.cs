using AutoMapper;
using BookRoom.Service.Application.UseCases.UserPropagation;
using BookRoom.Service.Domain.Contract.Notifications;
using BookRoom.Service.Domain.Contract.UseCases;
using BookRoom.Service.Domain.Repositories;
using BookRoom.Services.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace BookRoom.Service.Application.UseCases.RoomPropagation
{
    public class PropagateRoomUseCase : IPropagateRoomUseCase
    {
        private readonly IRoomRepository _repository;        
        private readonly IMapper _mapper;
        private readonly ILogger<PropagateUserUseCase> _logger;
        private readonly IUpdateRoomBookRoomUseCase _propagateToBooks;

        public PropagateRoomUseCase(
            IRoomRepository repository, 
            IMapper mapper, 
            ILogger<PropagateUserUseCase> logger, 
            IUpdateRoomBookRoomUseCase propagateToBooks)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _propagateToBooks = propagateToBooks;
        }
        public async Task HandleAsync(RoomNotification request, CancellationToken cancellationToken)
        {
            try
            {
                var roomDb = await _repository.FindOneAsync(request.Id, cancellationToken);

                var roomEvent = _mapper.Map<Room>(request);
                if (roomDb != null)
                {
                    roomEvent.Books = roomDb.Books;
                    await _repository.UpdateAsync(roomEvent, cancellationToken);
                }
                else
                {
                    await _repository.InsertAsync(roomEvent, cancellationToken);
                }

                roomDb = await _repository.FindOneAsync(request.Id, cancellationToken);

                var propagateToBooks = _mapper.Map<RoomNotification>(roomDb);

                await _propagateToBooks.HandleAsync(propagateToBooks, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{UseCase} - {Method} - Exception Thrown", nameof(PropagateUserUseCase), nameof(HandleAsync));
                //Implement super resilience process here
            }
        }
    }
}
