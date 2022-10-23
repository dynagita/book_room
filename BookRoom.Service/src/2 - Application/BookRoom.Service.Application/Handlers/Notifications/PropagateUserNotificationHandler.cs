using BookRoom.Service.Domain.Contract.Handlers.Notifications;
using BookRoom.Service.Domain.Contract.Notifications;
using BookRoom.Service.Domain.Contract.UseCases;

namespace BookRoom.Service.Application.Handlers.Notifications
{
    public class PropagateUserNotificationHandler : IPropagateUserNotificationHandler
    {
        private readonly IPropagateUserUseCase _useCase;

        public PropagateUserNotificationHandler(IPropagateUserUseCase useCase)
        {
            _useCase = useCase;
        }

        public async Task Handle(PropagateUserNotification notification, CancellationToken cancellationToken)
        {
            await _useCase.HandleAsync(notification, cancellationToken);
        }
    }
}
