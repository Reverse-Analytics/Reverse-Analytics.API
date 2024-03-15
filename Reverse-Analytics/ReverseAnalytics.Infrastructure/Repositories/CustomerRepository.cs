using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.ResourceParameters;
using ReverseAnalytics.Infrastructure.Helpers;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Infrastructure.Repositories
{
    public class CustomerRepository(ApplicationDbContext context) : RepositoryBase<Customer>(context), ICustomerRepository
    {
        public async Task<IEnumerable<Customer>> FindAllCustomersAsync(CustomerResourceParameters resourceParameters)
        {
            if (resourceParameters is null)
            {
                return await _context.Customers.ToListAsync();
            }

            var query = _context.Customers
                .AsQueryable()
                .AsNoTracking();

            if (!string.IsNullOrEmpty(resourceParameters.SearchQuery))
            {
                var searchQuery = resourceParameters.SearchQuery;
                query = query.Where(
                    x => x.FirstName.Contains(searchQuery) ||
                    x.LastName.Contains(searchQuery) ||
                    x.PhoneNumber.Contains(searchQuery) ||
                    (x.Company != null && x.Company.Contains(searchQuery)));
            }

            if (resourceParameters.Balance is not null)
            {
                query = query.Where(x => x.Balance == resourceParameters.Balance);
            }

            query = query.OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName);

            var pagedList = await PagedList<Customer>.CreateAsync(
                query,
                resourceParameters.PageNumber,
                resourceParameters.PageSize);

            return pagedList;
        }

        public override Task<IEnumerable<Customer>> FindAllAsync<CustomerResourceParametesrs>(CustomerResourceParametesrs resourceParameters)
        {
            throw new NotImplementedException();
        }
    }
}
