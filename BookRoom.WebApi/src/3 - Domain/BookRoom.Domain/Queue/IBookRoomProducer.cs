using BookRoom.Domain.Entities;

namespace BookRoom.Domain.Queue
{
    public interface IBookRoomProducer : IProducer<BookRooms>
    {
    }
}
