using BookRoom.Service.Domain.Contract.Handlers.Notifications;
using BookRoom.Service.Domain.Contract.Notifications;
using BookRoom.Service.Domain.Contract.UseCases;

namespace BookRoom.Service.Application.Handlers.Notifications
{
    public class PropagateRoomNotificationHandler : IPropagateRoomNotificationHandler
    {
        private readonly IPropagateRoomUseCase _useCase;

        public PropagateRoomNotificationHandler(IPropagateRoomUseCase useCase)
        {
            _useCase = useCase;
        }

        public async Task Handle(RoomNotification notification, CancellationToken cancellationToken)
        {
            await _useCase.HandleAsync(notification, cancellationToken);
        }
    }
}
