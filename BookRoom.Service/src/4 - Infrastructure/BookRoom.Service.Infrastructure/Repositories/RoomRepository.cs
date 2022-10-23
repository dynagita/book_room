using BookRoom.Service.Domain.Contract.Configurations;
using BookRoom.Service.Domain.Repositories;
using BookRoom.Services.Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookRoom.Service.Infrastructure.Repositories
{
    public class RoomRepository : RepositoryBase<Room>, IRoomRepository
    {
        public RoomRepository(IOptions<MongoConfiguration> mongoConfigurationOpt) : base(mongoConfigurationOpt)
        {
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
