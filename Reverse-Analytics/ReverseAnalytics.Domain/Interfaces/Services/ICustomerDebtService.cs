using ReverseAnalytics.Domain.DTOs.CustomerDebt;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface ICustomerDebtService
    {
        public Task<IEnumerable<CustomerDebtDto>?> GetAllCustomerDebtsAsync();
        public Task<CustomerDebtDto?> GetCustomerDebtByIdAsync(int id);
        public Task<CustomerDebtDto?> CreateCustomerDebtAsync(CustomerDebtForCreate customerDebtToCreate);
        public Task UpdateCustomerDebtAsync(CustomerDebtForUpdate customerDebtToUpdate);
        public Task DeleteCustomerDebtAsync(int id);
    }
}
