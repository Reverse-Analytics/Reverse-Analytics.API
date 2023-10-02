using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    public class SupplyItemRepository : RepositoryBase<SupplyItem>, ISupplyItemRepository
    {
        public SupplyItemRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<SupplyItem>> FindAllBySupplyIdAsync(int supplyId)
        {
            var supplyItems = await _context.SupplyItems
                                              .Where(sd => sd.SupplyId == supplyId)
                                              .ToListAsync();

            return supplyItems;
        }

        public async Task<IEnumerable<SupplyItem>> FindAllByProductIdAsync(int productId)
        {
            var supplyItems = await _context.SupplyItems
                                              .Where(sd => sd.ProductId == productId)
                                              .ToListAsync();

            return supplyItems;
        }

        public async Task<SupplyItem> FindBySupplyAndDetailIdAsync(int supplyId, int detailId)
        {
            var supplyDetail = await _context.SupplyItems
                .FirstOrDefaultAsync(s => s.SupplyId == supplyId && s.Id == detailId);

            return supplyDetail;
        }
    }
}
