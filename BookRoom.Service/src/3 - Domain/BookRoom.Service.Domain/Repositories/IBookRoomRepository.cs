using BookRoom.Services.Domain.Entities;

namespace BookRoom.Service.Domain.Repositories
{
    public interface IBookRoomRepository : IRepositoryBase<BookRooms>
    {
        Task<IList<BookRooms>> GetAllByUserAsync(int userId, CancellationToken cancellationToken);

        Task<IList<BookRooms>> GetAllByRoomAsync(int roomId, CancellationToken cancellationToken);
    }
}
