using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    public class CustomerPhoneRepository : RepositoryBase<CustomerPhone>, ICustomerPhoneRepository
    {
        public CustomerPhoneRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<CustomerPhone>> FindAllByCustomerId(int customerId)
        {
            var customerPhones = await _context.CustomerPhones
                                               .Where(x => x.CustomerId == customerId)
                                               .ToListAsync();

            return customerPhones;
        }

        public async Task<CustomerPhone?> FindByCustomerAndPhoneIdAsync(int customerId, int phoneId)
        {
            var customerPhone = await _context.CustomerPhones
                                              .FirstOrDefaultAsync(x => x.CustomerId == customerId && x.Id == phoneId);

            return customerPhone;
        }
    }
}
