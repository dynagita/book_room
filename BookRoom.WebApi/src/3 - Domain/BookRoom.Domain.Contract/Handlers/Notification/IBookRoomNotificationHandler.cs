using BookRoom.Domain.Contract.Notification.BookRooms;
using MediatR;

namespace BookRoom.Domain.Contract.Handlers.Notification
{
    public interface IBookRoomNotificationHandler : INotificationHandler<BookRoomNotification>
    {
    }
}
