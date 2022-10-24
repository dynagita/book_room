using BookRoom.Readness.Domain.Contract.Configurations;
using BookRoom.Readness.Domain.Entities;
using BookRoom.Readness.Domain.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Polly;
using Polly.Retry;

namespace BookRoom.Readness.Infrastructure.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        protected readonly IMongoCollection<T> _collection;
        private readonly AsyncRetryPolicy _retry;
        public RepositoryBase(IOptions<MongoConfiguration> mongoConfigurationOpt)
        {
            var mongoConfiguration = mongoConfigurationOpt.Value;

            var client = new MongoClient(mongoConfiguration.ConnectionString);
            var database = client.GetDatabase(mongoConfiguration.DatabaseName);

            _collection = database.GetCollection<T>(typeof(T).Name.ToLower());

            if (_collection == null)
            {
                database.CreateCollection(typeof(T).Name);
                _collection = database.GetCollection<T>(typeof(T).Name.ToLower());
            }

            _retry = Policy.Handle<Exception>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(3, retryAttempt)));
        }

        public async Task<T> FindOneAsync(long reference, CancellationToken cancellationToken)
        {
            var filter = Builders<T>.Filter.Eq(x => x.Id, reference);
            var data = await _retry.ExecuteAsync(async () => await _collection.FindAsync<T>(filter));
            return data.FirstOrDefault();
        }

        public async Task<List<T>> ListAllAsync(CancellationToken cancellationToken)
        {
            var filter = Builders<T>.Filter.Eq(x => x.Active, true);
            var data = await _retry.ExecuteAsync(async () => await _collection.FindAsync(filter));
            return await data.ToListAsync();
        }

        protected virtual FilterDefinition<T> GetEntityFilter(T entity)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(x => x.Id, entity.Id);

            return filter;
        }

        protected abstract UpdateDefinition<T> GetUpdateDefinition(T entity);
    }
}
