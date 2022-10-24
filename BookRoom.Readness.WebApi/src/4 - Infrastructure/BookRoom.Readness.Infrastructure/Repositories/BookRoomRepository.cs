using BookRoom.Readness.Domain.Contract.Configurations;
using BookRoom.Readness.Domain.Contract.Enums;
using BookRoom.Readness.Domain.Entities;
using BookRoom.Readness.Domain.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookRoom.Readness.Infrastructure.Repositories
{
    public class BookRoomRepository : RepositoryBase<BookRooms>, IBookRoomRepository
    {
        public BookRoomRepository(IOptions<MongoConfiguration> mongoConfigurationOpt) : base(mongoConfigurationOpt)
        {
        }

        public async Task<IList<BookRooms>> GetAllByUserAsync(int userId, CancellationToken cancellationToken)
        {
            var filter = Builders<BookRooms>.Filter.Eq(x => x.User.Id, userId);
            var cursor = await _collection.FindAsync(filter);
            return await cursor.ToListAsync();
        }

        public async Task<IList<BookRooms>> GetAllByRoomAsync(int roomId, CancellationToken cancellationToken)
        {
            var filter = Builders<BookRooms>.Filter.Eq(x => x.Room.Id, roomId);
            var cursor = await _collection.FindAsync(filter);
            return await cursor.ToListAsync();
        }

        protected override UpdateDefinition<BookRooms> GetUpdateDefinition(BookRooms entity)
        {
            var update = Builders<BookRooms>
                        .Update                        
                        .Set(x => x.Active, entity.Active)
                        .Set(x => x.DatAlt, entity.DatAlt)
                        .Set(x => x.DatInc, entity.DatInc)
                        .Set(x => x.EndDate, entity.EndDate)
                        .Set(x => x.Room, entity.Room)
                        .Set(x => x.StartDate, entity.StartDate)
                        .Set(x => x.Status, entity.Status)
                        .Set(x => x.User, entity.User);

            return update;
        }

        public async Task<IList<BookRooms>> GetAllByEmailAsync(string mail, CancellationToken cancellationToken)
        {
            var filter = Builders<BookRooms>.Filter.Eq(x => x.User.Email, mail);
            var cursor = await _collection.FindAsync(filter);
            return await cursor.ToListAsync();
        }

        public async Task<bool> CheckAvailabilityAsync(int roomNumber, DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
        {
            var query = _collection.AsQueryable();

            var data = query.Where(x => (
            (
                (startDate >= x.StartDate && startDate <= x.EndDate) || 
                (endDate >= x.StartDate && endDate <= x.EndDate)
            ) && x.Active == true &&
            x.Room.Number == roomNumber &&
            x.Status == BookStatusRoom.Confirmed));

            return !data.ToList().Any();
        }
    }
}
