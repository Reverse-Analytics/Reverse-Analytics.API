using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface IDebtRepository : IRepositoryBase<Debt>
    {
        public Task<IEnumerable<Debt>> FindAllByPersonIdAsync(int personId);
    }
}
