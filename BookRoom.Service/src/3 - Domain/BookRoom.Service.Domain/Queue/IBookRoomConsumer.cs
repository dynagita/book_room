using BookRoom.Service.Domain.Contract.Notifications;

namespace BookRoom.Service.Domain.Queue
{
    public interface IBookRoomConsumer : IConsumer<BookRoomsNotification>
    {
    }
}
