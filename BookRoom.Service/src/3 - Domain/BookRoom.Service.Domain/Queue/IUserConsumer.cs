using BookRoom.Services.Domain.Entities;

namespace BookRoom.Service.Domain.Queue
{
    public interface IUserConsumer : IConsumer<User>
    {
    }
}
