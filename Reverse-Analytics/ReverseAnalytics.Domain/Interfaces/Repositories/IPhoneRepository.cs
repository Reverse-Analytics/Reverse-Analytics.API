using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface IPhoneRepository : IRepositoryBase<Phone>
    {
        public Task<IEnumerable<Phone>> FindAllByPersonIdAsync(int personId);
        public Task<Phone> FindByPersonAndPhoneId(int personId, int phoneId);
    }
}
