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
        public async Task HandleAsync(PropagateRoomNotification request, CancellationToken cancellationToken)
        {
            try
            {
                var roomDb = await _repository.FindOneAsync(request.Reference, cancellationToken);

                var roomEvent = _mapper.Map<Room>(request);
                if (roomDb != null)
                {
                    roomEvent.Id = roomDb.Id;
                    await _repository.UpdateAsync(roomEvent, cancellationToken);
                }
                else
                {
                    await _repository.InsertAsync(roomEvent, cancellationToken);
                }

                roomDb = await _repository.FindOneAsync(request.Reference, cancellationToken);

                var propagateToBooks = _mapper.Map<PropagateRoomNotification>(roomDb);

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
