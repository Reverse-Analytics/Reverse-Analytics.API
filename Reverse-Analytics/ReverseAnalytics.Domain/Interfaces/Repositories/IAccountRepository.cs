using Microsoft.AspNetCore.Identity;

namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface IAccountRepository : IRepositoryBase<IdentityUser>
    {
    }
}
