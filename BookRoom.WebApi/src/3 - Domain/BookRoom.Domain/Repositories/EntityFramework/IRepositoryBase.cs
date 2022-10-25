namespace BookRoom.Domain.Repositories.EntityFramework
{
    public interface IRepositoryBase<T>
    {
        Task<T> GetAsync(int id, CancellationToken cancellationToken);

        Task<T> InsertAsync(T entity, CancellationToken cancellationToken);

        Task<T> UpdateAsync(int id, T entity, CancellationToken cancellationToken);

        Task<T> DeleteAsync(int id, CancellationToken cancellationToken);
    }

}
