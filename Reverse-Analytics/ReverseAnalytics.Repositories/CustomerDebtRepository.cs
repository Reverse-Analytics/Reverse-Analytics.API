using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    public class CustomerDebtRepository : RepositoryBase<CustomerDebt>, ICustomerDebtRepository
    {
        public CustomerDebtRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
