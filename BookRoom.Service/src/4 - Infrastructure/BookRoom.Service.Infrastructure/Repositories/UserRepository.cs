using BookRoom.Service.Domain.Contract.Configurations;
using BookRoom.Service.Domain.Repositories;
using BookRoom.Services.Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookRoom.Service.Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IOptions<MongoConfiguration> mongoConfigurationOpt) : base(mongoConfigurationOpt)
        {
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
