using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface ICustomerPhoneRepository : IRepositoryBase<CustomerPhone>
    {
        public Task<IEnumerable<CustomerPhone>> FindAllByCustomerId(int customerId);
        public Task<CustomerPhone?> FindByCustomerAndPhoneIdAsync(int customerId, int phoneId);
    }
}
