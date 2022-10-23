using BookRoom.Services.Domain.Entities;

namespace BookRoom.Service.Domain.Repositories
{
    public interface IRepositoryBase<T>
        where T : EntityBase
    {
        Task UpdateAsync(T entity);
        Task InsertAsync(T entity);
        Task DeleteAsync(T entity);

        Task<T> FindOneAsync(long reference);
    }
}
