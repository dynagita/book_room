using BookRoom.Application.UseCases.BookRoomUseCases;
using BookRoom.Domain.Contract.Handlers.Notification;
using BookRoom.Domain.Contract.Notification.BookRooms;
using BookRoom.Domain.Contract.UseCases.BookRooms;
using Microsoft.Extensions.DependencyInjection;

namespace BookRoom.Application.Handlers.Notifications
{
    public class BookRoomNotificationHandler : IBookRoomNotificationHandler
    {
        private readonly IBookRoomProcessUseCase _useCase;

        public BookRoomNotificationHandler(IBookRoomProcessUseCase useCase)
        {
            _useCase = useCase;
        }

        public async Task Handle(BookRoomNotification notification, CancellationToken cancellationToken)
        {
            await _useCase.HandleAsync(notification, cancellationToken);
        }
    }
}
