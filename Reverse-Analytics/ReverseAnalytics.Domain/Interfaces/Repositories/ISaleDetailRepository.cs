using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface ISaleDetailRepository : IRepositoryBase<SaleDetail>
    {
        public Task<IEnumerable<SaleDetail>> FindAllBySaleIdAsync(int saleId);
    }
}
