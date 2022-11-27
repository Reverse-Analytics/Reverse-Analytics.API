using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    public class SupplyRepository : RepositoryBase<Supply>, ISupplyRepository
    {
        public SupplyRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Supply>> FindAllBySupplierIdAsync(int supplierId)
        {
            var supplies = await _context.Supplies
                .Where(s => s.SupplierId == supplierId)
                .ToListAsync();

            return supplies;
        }

        public async Task<Supply> FindBySupplierAndSupplyIdAsync(int supplierId, int supplyId)
        {
            var supply = await _context.Supplies
                .FirstOrDefaultAsync(s => s.SupplierId == supplierId && s.Id == supplyId);

            return supply;
        }
    }
}
