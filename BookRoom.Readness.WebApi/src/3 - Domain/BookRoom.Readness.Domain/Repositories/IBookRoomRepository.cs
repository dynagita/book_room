using BookRoom.Readness.Domain.Entities;

namespace BookRoom.Readness.Domain.Repositories
{
    public interface IBookRoomRepository : IRepositoryBase<BookRooms>
    {
        Task<IList<BookRooms>> GetAllByUserAsync(int userId, CancellationToken cancellationToken);

        Task<IList<BookRooms>> GetAllByEmailAsync(string mail, CancellationToken cancellationToken);

        Task<IList<BookRooms>> GetAllByRoomAsync(int roomId, CancellationToken cancellationToken);

        Task<bool> CheckAvailabilityAsync(int roomNumber, DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
    }
}
