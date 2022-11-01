using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface ISupplyDetailRepository : IRepositoryBase<SupplyDetail>
    {
        public Task<IEnumerable<SupplyDetail>> FindAllBySupplyIdAsync(int supplyId);
        public Task<IEnumerable<SupplyDetail>> FindAllByProductIdAsync(int supplyId);
    }
}
