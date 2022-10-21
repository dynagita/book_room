using BookRoom.Domain.Entities;
using BookRoom.Domain.Repositories.EntityFramework;
using BookRoom.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Retry;

namespace BookRoom.Infrastructure.Database.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        protected DbContext _context { get; private set; }
        protected DbSet<T> _dbSet { get; private set; }
        private readonly AsyncRetryPolicy _retry;
        public RepositoryBase(BookRoomDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _retry = Policy.Handle<Exception>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(3, retryAttempt)));
        }

        public virtual async Task<T> GetAsync(int id, CancellationToken cancellationToken)
        {
            return await _retry.ExecuteAsync(async () => await _dbSet.FindAsync(id));
        }

        public virtual async Task<T> DeleteAsync(int id, CancellationToken cancellationToken)
        {

            var entityDb = _dbSet.Find(id);
            entityDb.Active = false;

            _context.Entry(entityDb).Property(x => x.Id).IsModified = false;
            _context.Entry(entityDb).CurrentValues.SetValues(entityDb);

            await _retry.ExecuteAsync(async () => await _context.SaveChangesAsync());

            return entityDb;

        }

        public virtual async Task<T> InsertAsync(T entity, CancellationToken cancellationToken)
        {
            entity.Active = true;
            entity.DatInc = entity.DatAlt = DateTime.Now;

            NormalizeForeignKeys(entity);

            await _dbSet.AddAsync(entity);
            await _retry.ExecuteAsync(async () => await _context.SaveChangesAsync());
            return entity;
        }

        public virtual async Task<T> UpdateAsync(int id, T entity, CancellationToken cancellationToken)
        {
            var entityDb = await _dbSet.FindAsync(id);
            entity.Id = entityDb.Id;
            entity.Active = entityDb.Active;

            NormalizeForeignKeys(entity);

            entity.DatAlt = DateTime.Now;

            _context.Entry(entityDb).Property(x => x.Id).IsModified = false;
            _context.Entry(entityDb).CurrentValues.SetValues(entity);
            await _retry.ExecuteAsync(async () => await _context.SaveChangesAsync());

            return entity;
        }

        protected virtual T NormalizeForeignKeys(T entity)
        {
            return entity;
        }
    }
}
