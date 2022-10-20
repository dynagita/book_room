using BookRoom.Domain.Entities;
using BookRoom.Domain.Repositories.EntityFramework;
using BookRoom.Infrastructure.Database.Context;

namespace BookRoom.Infrastructure.Database.Repositories
{
    public class BookRoomsRepository : RepositoryBase<BookRooms>, IBookRoomsRepository
    {
        public BookRoomsRepository(BookRoomDbContext context) : base(context)
        {
        }
    }
}
