using ReverseAnalytics.Domain.DTOs.CustomerPhoneDto;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>?> GetAllCustomerAsync(string? searchString, int pageNumber, int pageSize);
        Task<CustomerDto?> GetCustomerByIdAsync(int id);
        Task<CustomerDto?> CreateCustomerAsync(CustomerForCreateDto customerToCreate);
        Task UpdateCustomerAsync(CustomerForUpdateDto customerToUpdate);
        Task DeleteCustomerAsync(int id);
    }
}