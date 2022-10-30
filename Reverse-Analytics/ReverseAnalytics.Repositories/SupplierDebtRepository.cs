using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    public class SupplierDebtRepository : RepositoryBase<SupplierDebt>, ISupplierDebtRepository
    {
        public SupplierDebtRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
