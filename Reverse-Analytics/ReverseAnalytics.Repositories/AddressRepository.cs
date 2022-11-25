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

        public async Task<IEnumerable<Address>> FindAllByPersonIdAsync(int personId)
        {
            var addresses = await _context.Addresses
                .Where(a => a.PersonId == personId)
                .ToListAsync();

            return addresses;
        }

        public async Task<Address?> FindByPersonAndAddressIdAsync(int personId, int addressId)
        {
            var address = await _context.Addresses
                .FirstOrDefaultAsync(a => a.PersonId == personId &&
                    a.Id == addressId);

            return address;
        }
    }
}
