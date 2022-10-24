using BookRoom.Readness.Domain.Entities;

namespace BookRoom.Readness.Domain.Repositories
{
    public interface IRoomRepository : IRepositoryBase<Room>
    {
        Task<Room> GetByRoomNumberAsync(int roomNumber, CancellationToken cancellationToken);
    }
}
