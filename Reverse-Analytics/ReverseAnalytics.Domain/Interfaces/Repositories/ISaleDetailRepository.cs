using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface ISaleDetailRepository : IRepositoryBase<SaleDetail>
    {
        public async Task<IEnumerable<SaleDetail>> FindAllBySaleId(int saleId)
    }
}
