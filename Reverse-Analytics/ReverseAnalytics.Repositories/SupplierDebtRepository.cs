using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    public class SupplierDebtRepository : RepositoryBase<SupplierDebt>, ISupplierDebtRepository
    {
        public SupplierDebtRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<SupplierDebt?> FindBySupplierAndDebtIdAsync(int supplierId, int debtId)
        {
            var supplierDebt = await _context.SupplierDebts
                                             .FirstOrDefaultAsync(sd => sd.SupplierId == supplierId && sd.Id == debtId);

            return supplierDebt;
        }

        public async Task<IEnumerable<SupplierDebt>> FindAllBySupplierIdAsync(int supplierId)
        {
            var supplierDebts = await _context.SupplierDebts
                .Where(sd => sd.SupplierId == supplierId)
                .ToListAsync();

            return supplierDebts;
        }
    }
}
