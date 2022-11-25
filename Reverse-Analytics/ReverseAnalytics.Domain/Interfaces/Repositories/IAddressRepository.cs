using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface IAddressRepository : IRepositoryBase<Address>
    {
        public Task<IEnumerable<Address>> FindAllByPersonIdAsync(int personId);
        public Task<Address?> FindByPersonAndAddressIdAsync(int personId, int addressId);
    }
}
