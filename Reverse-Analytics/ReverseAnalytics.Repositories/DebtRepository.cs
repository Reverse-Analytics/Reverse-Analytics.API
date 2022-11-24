using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    internal class DebtRepository : RepositoryBase<Debt>, IDebtRepository
    {
        public DebtRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
