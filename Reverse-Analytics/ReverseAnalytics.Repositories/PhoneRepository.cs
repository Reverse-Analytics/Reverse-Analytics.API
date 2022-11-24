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

        public async Task<IEnumerable<Phone>> FindAllByPersonId(int personId)
        {
            var phones = await _context.Phones
                .Where(p => p.PersonId == personId)
                .ToListAsync();

            return phones;
        }
    }
}
