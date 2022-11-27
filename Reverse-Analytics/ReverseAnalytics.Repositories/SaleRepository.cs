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

        public async Task<IEnumerable<Sale>> FindAllByCustomerIdAsync(int customerId)
        {
            var sales = await _context.Sales
                .Where(s => s.CustomerId == customerId)
                .AsNoTracking()
                .ToListAsync();

            return sales;
        }

        public async Task<Sale> FindByCustomerAndSaleIdAsync(int customerId, int saleId)
        {
            var sale = await _context.Sales
                .FirstOrDefaultAsync(s => s.CustomerId == customerId && s.Id == saleId);

            return sale;
        }
    }
}