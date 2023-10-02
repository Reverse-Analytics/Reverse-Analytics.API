using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    public class SaleRepository : RepositoryBase<Sale>, ISaleRepository
    {
        public SaleRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Sale>> FindAllAsync()
        {
            return await _context.Sales
                .Include(x => x.Customer)
                .Include(x => x.SaleItems)
                .ThenInclude(x => x.Product)
                .Include(x => x.Refunds)
                .Include(x => x.SaleDebt)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Sale>> FindAllByCustomerIdAsync(int customerId)
        {
            return await _context.Sales
                .Where(x => x.CustomerId == customerId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}