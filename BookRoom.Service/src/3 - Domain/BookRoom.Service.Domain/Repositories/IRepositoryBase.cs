using BookRoom.Services.Domain.Entities;

namespace BookRoom.Service.Domain.Repositories
{
    public interface IRepositoryBase<T>
        where T : EntityBase
    {
        Task UpdateAsync(T entity, CancellationToken cancellationToken);
        Task InsertAsync(T entity, CancellationToken cancellationToken);
        Task DeleteAsync(T entity, CancellationToken cancellationToken);

        Task<T> FindOneAsync(long reference, CancellationToken cancellationToken);
    }
}
