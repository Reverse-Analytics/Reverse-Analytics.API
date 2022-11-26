using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface ICustomerDebtRepository : IRepositoryBase<CustomerDebt>
    {
        public Task<IEnumerable<CustomerDebt>> FindAllByCustomerId(int customerId);
    }
}
