using BookRoom.Domain.Entities;
using BookRoom.Domain.Repositories.EntityFramework;
using BookRoom.Infrastructure.Database.Context;

namespace BookRoom.Infrastructure.Database.Repositories
{
    public class RoomRepository : RepositoryBase<Room>, IRoomRepository
    {
        public RoomRepository(BookRoomDbContext context) : base(context)
        {
        }
    }
}
