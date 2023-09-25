using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface ISaleItemRepository : IRepositoryBase<SaleItem>
    {
        public Task<IEnumerable<SaleItem>> FindAllBySaleIdAsync(int saleId);
        public Task<SaleItem> FindBySaleAndDetailIdAsync(int saleId, int detailId);
        public Task DeleteRangeBySaleIdAsync(int saleId);
    }
}
