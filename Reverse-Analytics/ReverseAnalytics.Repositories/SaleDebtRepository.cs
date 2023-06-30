using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    public class SaleDebtRepository : RepositoryBase<SaleDebt>, ISaleDebtRepository
    {
        public SaleDebtRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
