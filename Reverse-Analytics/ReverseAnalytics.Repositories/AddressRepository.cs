using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    public class AddressRepository : RepositoryBase<Address>, IAddressRepository
    {
        public AddressRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Address>> FindAllByPersonId(int id)
        {
            var addresses = await _context.Addresses
                .Where(a => a.PersonId == id)
                .ToListAsync();

            return addresses;
        }
    }
}
