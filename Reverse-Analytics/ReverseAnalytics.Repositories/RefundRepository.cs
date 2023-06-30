using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    public class RefundRepository : RepositoryBase<Refund>, IRefundRepository
    {
        public RefundRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
