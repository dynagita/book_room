using BookRoom.Domain.Entities;

namespace BookRoom.Domain.Repositories.EntityFramework
{
    public interface IRoomRepository : IRepositoryBase<Room>
    {
        Task<Room> GetByNumber(int number, CancellationToken cancellationToken);
    }
}
