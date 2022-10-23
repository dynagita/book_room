using BookRoom.Service.Domain.Contract.Configurations;
using BookRoom.Service.Domain.Repositories;
using BookRoom.Services.Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Polly;
using Polly.Retry;

namespace BookRoom.Service.Infrastructure.Repositories
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

        public async Task DeleteAsync(T entity)
        {
            await _retry.ExecuteAsync(async () => await _collection.DeleteOneAsync(GetEntityFilter(entity)));
        }

        public async Task<T> FindOneAsync(long reference)
        {
            var filter = Builders<T>.Filter.Eq(x => x.Reference, reference);
            var data = await _retry.ExecuteAsync(async () => await _collection.FindAsync<T>(filter));
            return data.FirstOrDefault();
        }

        public async Task InsertAsync(T entity)
        {
            await _retry.ExecuteAsync(async () => await _collection.InsertOneAsync(entity));
        }

        public async Task UpdateAsync(T entity)
        {
            await _retry.ExecuteAsync(async () => await _collection.UpdateOneAsync(GetEntityFilter(entity), GetUpdateDefinition(entity)));
        }

        protected virtual FilterDefinition<T> GetEntityFilter(T entity)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(x => x.Reference, entity.Reference);

            return filter;
        }

        protected abstract UpdateDefinition<T> GetUpdateDefinition(T entity);
    }
}
