using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    public class SaleDetailRepository : RepositoryBase<SaleDetail>, ISaleDetailRepository
    {
        public SaleDetailRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<SaleDetail>> FindAllBySaleIdAsync(int saleId)
        {
            var saleDetails = await _context.SaleDetails
                .Where(s => s.SaleId == saleId)
                .ToListAsync();

            return saleDetails;
        }

        public async Task<SaleDetail> FindBySaleAndDetailIdAsync(int saleId, int detailId)
        {
            var saleDetail = await _context.SaleDetails
                .FirstOrDefaultAsync(s => s.SaleId == saleId && s.Id == detailId);

            return saleDetail;
        }
    }
}
