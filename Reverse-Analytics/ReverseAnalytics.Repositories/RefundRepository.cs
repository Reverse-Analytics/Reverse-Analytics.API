using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Refund>> FindRefundsAsync()
        {
            return await _context.Set<Refund>()
                                 .Include(x => x.RefundDetails)
                                 .ThenInclude(x => x.Product)
                                 .Include(x => x.Sale)
                                 .ThenInclude(s => s.Customer)
                                 .Include(x => x.Sale)
                                 .ThenInclude(s => s.SaleDetails)
                                 .ToListAsync();
        }
    }
}
