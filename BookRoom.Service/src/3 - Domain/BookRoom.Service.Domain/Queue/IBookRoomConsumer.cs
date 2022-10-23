using BookRoom.Services.Domain.Entities;

namespace BookRoom.Service.Domain.Queue
{
    public interface IBookRoomConsumer : IConsumer<BookRooms>
    {
    }
}
