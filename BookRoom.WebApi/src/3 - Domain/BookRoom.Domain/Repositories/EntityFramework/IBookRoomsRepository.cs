using BookRoom.Domain.Entities;

namespace BookRoom.Domain.Repositories.EntityFramework
{
    public interface IBookRoomsRepository : IRepositoryBase<BookRooms>
    {
        Task<BookRooms> GetByRoomAsync(int roomId, CancellationToken cancellationToken);

        Task<BookRooms> GetBookRoomByPeriod(int roomId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
    }
}
