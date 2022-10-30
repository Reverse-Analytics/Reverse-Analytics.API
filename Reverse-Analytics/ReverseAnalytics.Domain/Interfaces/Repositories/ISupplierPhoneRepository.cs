using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface ISupplierPhoneRepository : IRepositoryBase<SupplierPhone>
    {
        public Task<IEnumerable<SupplierPhone>> FindAllBySupplierIdAsync(int supplierId);
    }
}
