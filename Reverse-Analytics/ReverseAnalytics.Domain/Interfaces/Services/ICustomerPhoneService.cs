using ReverseAnalytics.Domain.DTOs.CustomerPhone;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface ICustomerPhoneService
    {
        public Task<IEnumerable<CustomerPhoneDto>?> GetCustomerPhonesAsync();
        public Task<IEnumerable<CustomerPhoneDto>?> GetCustomerPhonesByCustomerIdAsync(int customerId);
        public Task<CustomerPhoneDto?> GetCustomerPhoneByCustomerAndPhoneIdAsync(int customerId, int phoneId);
        public Task<CustomerPhoneDto> CreateCustomerPhoneAsync(CustomerPhoneForCreate customerPhoneToCreate);
        public Task UpdateCustomerPhoneAsync(CustomerPhoneForUpdate customerPhoneToUpdate);
        public Task DeleteCustomerPhoneAsync(int phoneId);
    }
}
