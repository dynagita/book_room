using BookRoom.Domain.Entities;

namespace BookRoom.Domain.Queue
{
    public interface IUserProducer : IProducer<User>
    {
    }
}
