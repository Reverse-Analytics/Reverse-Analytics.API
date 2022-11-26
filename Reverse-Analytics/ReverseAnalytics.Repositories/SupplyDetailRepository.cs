using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    public class SupplyDetailRepository : RepositoryBase<SupplyDetail>, ISupplyDetailRepository
    {
        public SupplyDetailRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<SupplyDetail>> FindAllBySupplyIdAsync(int supplyId)
        {
            var supplyDetails = await _context.SupplyDetails
                                              .Where(sd => sd.SupplyId == supplyId)
                                              .ToListAsync();

            return supplyDetails;
        }

        public async Task<IEnumerable<SupplyDetail>> FindAllByProductIdAsync(int productId)
        {
            var supplyDetails = await _context.SupplyDetails
                                              .Where(sd => sd.ProductId == productId)
                                              .ToListAsync();

            return supplyDetails;
        }
    }
}
