using BookRoom.Readness.Domain.Contract.Configurations;
using BookRoom.Readness.Domain.Entities;
using BookRoom.Readness.Domain.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookRoom.Readness.Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IOptions<MongoConfiguration> mongoConfigurationOpt) : base(mongoConfigurationOpt)
        {
        }

        public async Task<User> GetByMailAsync(string mail, CancellationToken cancellationToken)
        {
            var filter = Builders<User>.Filter.Eq(x => x.Email, mail);
            return await _collection.FindSync(filter).FirstOrDefaultAsync();
        }

        protected override UpdateDefinition<User> GetUpdateDefinition(User entity)
        {
            var update = Builders<User>
                        .Update
                        .Set(x => x.Name, entity.Name)
                        .Set(x => x.LastName, entity.LastName)
                        .Set(x => x.Email, entity.Email)
                        .Set(x => x.BornDate, entity.BornDate)
                        .Set(x => x.Active, entity.Active)
                        .Set(x => x.Books, entity.Books)
                        .Set(x => x.DatAlt, entity.DatAlt)
                        .Set(x => x.DatInc, entity.DatInc)
                        .Set(x => x.Active, entity.Active);
            
            return update;
        }
    }
}
