using BookRoom.Service.Domain.Contract.Configurations;
using BookRoom.Service.Domain.Repositories;
using BookRoom.Services.Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookRoom.Service.Infrastructure.Repositories
{
    public class BookRoomRepository : RepositoryBase<BookRooms>, IBookRoomRepository
    {
        public BookRoomRepository(IOptions<MongoConfiguration> mongoConfigurationOpt) : base(mongoConfigurationOpt)
        {
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
    }
}
