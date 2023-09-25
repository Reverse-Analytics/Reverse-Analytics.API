using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface IRefundItemRepository : IRepositoryBase<RefundItem>
    {
        public Task<IEnumerable<RefundItem>> FindAllByRefundIdAsync(int refundId);
    }
}
