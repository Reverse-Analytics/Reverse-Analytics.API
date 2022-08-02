using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface ICustomerRepository : IRepositoryBase<Customer>
    {
        public Task<IEnumerable<Customer>> FindAllCustomers(string? searchString);
    }
}
