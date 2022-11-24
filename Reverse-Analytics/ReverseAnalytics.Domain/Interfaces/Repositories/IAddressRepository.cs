using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface IAddressRepository : IRepositoryBase<Address>
    {
        public Task<IEnumerable<Address>> FindAllByPersonId(int id);
    }
}
