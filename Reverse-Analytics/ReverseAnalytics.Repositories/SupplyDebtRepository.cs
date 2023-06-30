using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    public class SupplyDebtRepository : RepositoryBase<SupplyDebt>, ISupplyDebtRepository
    {
        public SupplyDebtRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<SupplyDebt>> FindAllBySupplyIdAsync(int supplyId)
        {
            return await _context.SupplyDebts
                .Where(x => x.SupplyId == supplyId)
                .ToListAsync();
        }
    }
}
