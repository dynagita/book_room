using BookRoom.Domain.Entities;
using BookRoom.Domain.Repositories.EntityFramework;
using BookRoom.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace BookRoom.Infrastructure.Database.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(BookRoomDbContext context) : base(context)
        {
        }

        public async Task<User> GetByMailAsync(string email, CancellationToken cancellationToken)
        {
            return await _dbSet.Where(x => 
            x.Email
             .Trim()
             .ToLower()
             .Equals(email
                    .Trim()
                    .ToLower()))
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
