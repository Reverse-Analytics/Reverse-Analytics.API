using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface ISupplyDebtRepository : IRepositoryBase<SupplyDebt>
    {
        public Task<IEnumerable<SupplyDebt>> FindAllBySupplyIdAsync(int supplyId);
    }
}
