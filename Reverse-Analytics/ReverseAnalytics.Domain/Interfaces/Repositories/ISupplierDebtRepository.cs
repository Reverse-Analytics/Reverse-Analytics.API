using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface ISupplierDebtRepository : IRepositoryBase<SupplierDebt>
    {
        public Task<IEnumerable<SupplierDebt>> FindAllBySupplierIdAsync(int supplierId);
        public Task<SupplierDebt> FindAllBySupplierAndDebtIdAsync(int supplierId, int debtId);
    }
}
