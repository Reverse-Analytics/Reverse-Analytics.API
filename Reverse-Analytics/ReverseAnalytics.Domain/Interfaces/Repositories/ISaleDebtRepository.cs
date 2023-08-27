using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface ISaleDebtRepository : IRepositoryBase<SaleDebt>
    {
        public Task<IEnumerable<SaleDebt>> FindAllBySaleIdAsync(int saleId);
        public Task<IEnumerable<SaleDebt>> FindAllAsync();
    }
}
