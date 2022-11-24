using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    internal class DebtRepository : RepositoryBase<Debt>, IDebtRepository
    {
        public DebtRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Debt>> FindAllByPersonIdAsync(int personId)
        {
            var debts = await _context.Debts
                .Where(d => d.PersonId == personId)
                .ToListAsync();

            return debts;
        }
    }
}
