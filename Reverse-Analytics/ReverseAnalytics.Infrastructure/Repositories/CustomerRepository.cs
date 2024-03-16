using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Infrastructure.Repositories;

public class CustomerRepository(ApplicationDbContext context) : RepositoryBase<Customer>(context), ICustomerRepository
{
}
