using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    public class InventoryDetailRepository : RepositoryBase<InventoryDetail>, IInventoryDetailRepository
    {
        public InventoryDetailRepository(ApplicationDbContext context) 
            : base(context)
        {
        }

        public async Task<IEnumerable<InventoryDetail>> FindAllByInventoryIdAsync(int inventoryId)
        {
            var inventorDetails = await _context.InventoryDetails
                .Where(id => id.InventoryId == inventoryId)
                .ToListAsync();

            return inventorDetails;
        }

        public async Task<InventoryDetail> FindByInventoryAndDetailIdAsync(int inventoryId, int detailId)
        {
            var inventoryDetail = await _context.InventoryDetails
                .FirstOrDefaultAsync(i => i.InventoryId == inventoryId && i.Id == detailId);

            return inventoryDetail;
        }
    }
}
