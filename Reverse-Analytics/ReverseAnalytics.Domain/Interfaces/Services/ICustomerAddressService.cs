using ReverseAnalytics.Domain.DTOs.CustomerAddress;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface ICustomerAddressService
    {
        public Task<IEnumerable<CustomerAddressDto>?> GetCustomerAddressesAsync();
        public Task<CustomerAddressDto?> GetCustomerAddressByIdAsync(int customerAddressId);
        public Task<CustomerAddressDto?> CreateCustomerAddressAsync(CustomerAddressForCreateDto customerAddressToCreate);
        public Task UpdateCustomerAddressAsync(CustomerAddressForUpdateDto customerAddressToUpdate);
        public Task DeleteCustomerAddressAsync(int id);
    }
}
