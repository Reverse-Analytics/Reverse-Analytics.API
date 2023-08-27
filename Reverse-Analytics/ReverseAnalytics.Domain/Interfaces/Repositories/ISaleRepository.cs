using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface ISaleRepository : IRepositoryBase<Sale>
    {
        public Task<IEnumerable<Sale>> FindAllAsync();
        public Task<IEnumerable<Sale>> FindAllByCustomerIdAsync(int customerId);
    }
}
