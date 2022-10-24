using BookRoom.Readness.Domain.Entities;

namespace BookRoom.Readness.Domain.Repositories
{
    public interface IRepositoryBase<T>
        where T : EntityBase
    {
        Task<T> FindOneAsync(long reference, CancellationToken cancellationToken);

        Task<List<T>> ListAllAsync(CancellationToken cancellationToken);
    }
}
