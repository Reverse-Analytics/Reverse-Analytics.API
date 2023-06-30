using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    internal class RefundDetailRepository : RepositoryBase<RefundDetail>, IRefundDetailRepository
    {
        public RefundDetailRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
