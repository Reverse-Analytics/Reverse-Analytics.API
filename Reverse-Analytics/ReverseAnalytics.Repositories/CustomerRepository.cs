using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<IEnumerable<Customer>> FindAllCustomers(string? searchString, int pageNumber, int pageSize)
        {
            var customers = _context.Customers
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(c => c.FullName.Contains(searchString));
            }

            customers = customers.OrderBy(c => c.FullName)
                .ThenBy(c => c.IsActive);

            customers = customers.Skip(pageSize * (pageNumber - 1))
                .Take(pageSize);

            return await customers.ToListAsync();
        }
    }
}
