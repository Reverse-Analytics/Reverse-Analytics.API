using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<User?> FindByNameAndPassword(string userName, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password);

            return user;
        }
    }
}
