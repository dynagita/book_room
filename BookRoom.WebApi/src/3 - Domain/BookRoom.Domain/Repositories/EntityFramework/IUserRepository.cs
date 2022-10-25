using BookRoom.Domain.Entities;

namespace BookRoom.Domain.Repositories.EntityFramework
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User> GetByMailAsync(string email, CancellationToken cancellationToken);
    }
}
