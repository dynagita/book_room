using BookRoom.Domain.Entities;
using BookRoom.Domain.Repositories.EntityFramework;
using BookRoom.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace BookRoom.Infrastructure.Database.Repositories
{
    public class RoomRepository : RepositoryBase<Room>, IRoomRepository
    {
        public RoomRepository(BookRoomDbContext context) : base(context)
        {
        }

        public async Task<Room> GetByNumber(int number, CancellationToken cancellationToken)
        {
            return await _dbSet.Where(x =>
            x.Number == number)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
