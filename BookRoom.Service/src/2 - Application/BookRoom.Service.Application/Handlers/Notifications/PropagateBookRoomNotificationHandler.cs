using BookRoom.Service.Domain.Contract.Handlers.Notifications;
using BookRoom.Service.Domain.Contract.Notifications;
using BookRoom.Service.Domain.Contract.UseCases;

namespace BookRoom.Service.Application.Handlers.Notifications
{
    public class PropagateBookRoomNotificationHandler : IPropagateBookRoomNotificationHandler
    {
        private readonly IPropagateBookRoomUseCase _useCase;

        public PropagateBookRoomNotificationHandler(IPropagateBookRoomUseCase useCase)
        {
            _useCase = useCase;
        }

        public async Task Handle(BookRoomsNotification notification, CancellationToken cancellationToken)
        {
            await _useCase.HandleAsync(notification, cancellationToken);
        }
    }
}
