using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface IInventoryDetailRepository : IRepositoryBase<InventoryDetail>
    {
        public Task<IEnumerable<InventoryDetail>> FindAllByInventoryIdAsync(int inventoryId);
        public Task<InventoryDetail> FindByInventoryAndDetailIdAsync(int inventoryId, int detailId);
    }
}
