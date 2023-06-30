using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface IRefundDetailRepository : IRepositoryBase<RefundDetail>
    {
        public Task<IEnumerable<RefundDetail>> FindAllByRefundIdAsync(int refundId);
    }
}
