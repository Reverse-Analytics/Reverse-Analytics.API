using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    public class CustomerAddressRepository : RepositoryBase<CustomerAddress>, ICustomerAddressRepository
    {
        public CustomerAddressRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
