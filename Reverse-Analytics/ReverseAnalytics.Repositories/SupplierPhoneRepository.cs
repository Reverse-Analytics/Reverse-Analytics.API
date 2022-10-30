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
    }
}
