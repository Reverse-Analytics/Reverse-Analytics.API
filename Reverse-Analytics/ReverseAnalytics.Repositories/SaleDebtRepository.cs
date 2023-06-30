using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<SaleDebt>> FindAllBySaleIdAsync(int saleId)
        {
            return await _context.SaleDebts
                .Where(x => x.SaleId == saleId)
                .ToListAsync();
        }
    }
}
