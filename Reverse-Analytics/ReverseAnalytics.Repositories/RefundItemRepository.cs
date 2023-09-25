using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    public class RefundItemRepository : RepositoryBase<RefundItem>, IRefundItemRepository
    {
        public RefundItemRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<RefundItem>> FindAllByRefundIdAsync(int refundId)
        {
            return await _context.RefundItems
                .Where(x => x.RefundId == refundId)
                .ToListAsync();
        }
    }
}
