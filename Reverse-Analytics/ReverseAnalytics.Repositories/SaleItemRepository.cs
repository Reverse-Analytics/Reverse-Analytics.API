using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    public class SaleItemRepository : RepositoryBase<SaleItem>, ISaleItemRepository
    {
        public SaleItemRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<SaleItem>> FindAllBySaleIdAsync(int saleId)
        {
            var saleItems = await _context.SaleItems
                .Where(s => s.SaleId == saleId)
                .ToListAsync();

            return saleItems;
        }

        public async Task<SaleItem> FindBySaleAndDetailIdAsync(int saleId, int detailId)
        {
            var saleDetail = await _context.SaleItems
                .FirstOrDefaultAsync(s => s.SaleId == saleId && s.Id == detailId);

            return saleDetail;
        }

        public async Task DeleteRangeBySaleIdAsync(int saleId)
        {
            var saleItems = await _context.SaleItems
                .Where(x => x.SaleId == saleId)
                .ToListAsync();

            if (!saleItems.Any())
            {
                return;
            }

            _context.RemoveRange(saleItems);
        }
    }
}
