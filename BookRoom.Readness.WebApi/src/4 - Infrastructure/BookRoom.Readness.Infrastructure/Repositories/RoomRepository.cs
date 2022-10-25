using BookRoom.Readness.Domain.Contract.Configurations;
using BookRoom.Readness.Domain.Entities;
using BookRoom.Readness.Domain.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookRoom.Readness.Infrastructure.Repositories
{
    public class RoomRepository : RepositoryBase<Room>, IRoomRepository
    {
        public RoomRepository(IOptions<MongoConfiguration> mongoConfigurationOpt) : base(mongoConfigurationOpt)
        {
        }

        public async Task<Room> GetByRoomNumberAsync(int roomNumber, CancellationToken cancellationToken)
        {
            var filter = Builders<Room>.Filter.Eq(x => x.Number, roomNumber);
            var cursor = await _collection.FindAsync(filter);
            return await cursor.FirstOrDefaultAsync();
        }

        protected override UpdateDefinition<Room> GetUpdateDefinition(Room entity)
        {
            var update = Builders<Room>
                        .Update
                        .Set(x => x.Active, entity.Active)
                        .Set(x => x.Books, entity.Books)
                        .Set(x => x.DatAlt, entity.DatAlt)
                        .Set(x => x.DatInc, entity.DatInc)
                        .Set(x => x.Description, entity.Description)
                        .Set(x => x.Number, entity.Number)
                        .Set(x => x.Title, entity.Title);

            return update;
        }
    }
}
