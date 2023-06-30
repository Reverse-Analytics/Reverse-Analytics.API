using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    public class RefundDetailRepository : RepositoryBase<RefundDetail>, IRefundDetailRepository
    {
        public RefundDetailRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<RefundDetail>> FindAllByRefundIdAsync(int refundId)
        {
            return await _context.RefundDetails
                .Where(x => x.RefundId == refundId)
                .ToListAsync();
        }
    }
}
