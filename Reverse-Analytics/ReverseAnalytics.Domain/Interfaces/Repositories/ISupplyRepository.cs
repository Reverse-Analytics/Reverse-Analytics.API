using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface ISupplyRepository : IRepositoryBase<Supply>
    {
        public Task<IEnumerable<Supply>> FindAllBySupplierIdAsync(int supplierId);
    }
}
