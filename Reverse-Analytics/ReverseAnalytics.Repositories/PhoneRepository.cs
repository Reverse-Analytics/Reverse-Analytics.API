using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    public class PhoneRepository : RepositoryBase<Phone>, IPhoneRepository
    {
        public PhoneRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Phone>> FindAllByPersonIdAsync(int personId)
        {
            var phones = await _context.Phones
                .Where(p => p.PersonId == personId)
                .ToListAsync();

            return phones;
        }

        public async Task<Phone> FindByPersonAndPhoneId(int personId, int phoneId)
        {
            var phone = await _context.Phones
                .FirstOrDefaultAsync(p => p.PersonId == personId && p.Id == phoneId);

            return phone;
        }
    }
}
