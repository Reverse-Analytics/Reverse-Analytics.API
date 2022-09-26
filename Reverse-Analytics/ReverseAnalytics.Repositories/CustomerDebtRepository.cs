using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<CustomerDebt>> FindAllByCustomerId(int customerId)
        {
            var debts = await _context.CustomerDebts
                .Where(cd => cd.CustomerId == customerId)
                .AsNoTracking()
                .OrderByDescending(cd => cd.Id)
                .ToListAsync();

            return debts;
        }
    }
}
