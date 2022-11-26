using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    public class SupplierPhoneRepository : RepositoryBase<SupplierPhone>, ISupplierPhoneRepository
    {
        public SupplierPhoneRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<SupplierPhone>> FindAllBySupplierIdAsync(int supplierId)
        {
            var supplierPhones = await _context.SupplierPhones
                                               .Where(sp => sp.SupplierId == supplierId)
                                               .ToListAsync();

            return supplierPhones;
        }

        public async Task<SupplierPhone?> FindBySupplierAndPhoneIdAsync(int supplierId, int phoneId)
        {
            var supplierPhone = await _context.SupplierPhones.FirstOrDefaultAsync(sp => sp.SupplierId == supplierId && sp.Id == phoneId);

            return supplierPhone;
        }
    }
}
