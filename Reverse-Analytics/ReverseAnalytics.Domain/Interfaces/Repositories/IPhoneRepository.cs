using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface IPhoneRepository : IRepositoryBase<Phone>
    {
        public Task<IEnumerable<Phone>> FindAllByPersonId(int personId);
    }
}
