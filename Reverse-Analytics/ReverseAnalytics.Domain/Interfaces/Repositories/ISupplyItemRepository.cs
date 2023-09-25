using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface ISupplyItemRepository : IRepositoryBase<SupplyItem>
    {
        public Task<IEnumerable<SupplyItem>> FindAllBySupplyIdAsync(int supplyId);
        public Task<IEnumerable<SupplyItem>> FindAllByProductIdAsync(int supplyId);
        public Task<SupplyItem> FindBySupplyAndDetailIdAsync(int supplyId, int detailId);
    }
}
