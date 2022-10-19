namespace BookRoom.Domain.Repositories.EntityFramework
{
    public interface IRepositoryBase<T>
    {
        Task<T> InsertAsync(T entity, CancellationToken cancellationToken);
        Task<T> UpdateAsync(int id, T entity, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
