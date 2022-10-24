using BookRoom.Readness.Domain.Entities;

namespace BookRoom.Readness.Domain.Repositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User> GetByMailAsync(string mail, CancellationToken cancellationToken);
    }
}
