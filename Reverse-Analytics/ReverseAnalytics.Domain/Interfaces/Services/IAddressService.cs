using ReverseAnalytics.Domain.DTOs.Address;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface IAddressService
    {
        public Task<IEnumerable<AddressDto>> GetAllAddressesAsync();
        public Task<IEnumerable<AddressDto>> GetAddressByPersonIdAsync(int personId);
        public Task<AddressDto> GetAddressByIdAsync(int id);
        public Task<AddressDto> CreateAddressAsync(AddressForCreateDto addressToCreate);
        public Task UpdateAddresAsync(AddressForUpdateDto addressToUpdate);
        public Task DeleteAddressAsync(int id);
    }
}
