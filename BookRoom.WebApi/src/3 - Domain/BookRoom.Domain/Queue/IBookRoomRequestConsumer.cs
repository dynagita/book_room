using BookRoom.Domain.Contract.Notification.BookRooms;

namespace BookRoom.Domain.Queue
{
    public interface IBookRoomRequestConsumer : IConsumer<BookRoomNotification>
    {
    }
}
