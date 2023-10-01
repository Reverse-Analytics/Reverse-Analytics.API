using ReverseAnalytics.Domain.DTOs.Customer;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    /// <summary>
    /// Represents a service for managing customer-related operations.
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Retrieves a collection of customers based on search criteria.
        /// </summary>
        /// <param name="searchString">A string to filter customers by name or other attributes.</param>
        /// <param name="pageNumber">The page number of the results.</param>
        /// <param name="pageSize">The maximum number of items per page.</param>
        /// <returns>
        /// A collection of <see cref="CustomerDto"/> objects matching the search criteria.
        /// </returns>
        Task<IEnumerable<CustomerDto>> GetAllCustomerAsync(string? searchString, int pageNumber, int pageSize);

        /// <summary>
        /// Retrieves a customer by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the customer.</param>
        /// <returns>
        /// A <see cref="CustomerDto"/> representing the customer with the specified ID.
        /// </returns>
        /// <exception cref="NotFoundException">Thrown if the customer with the specified ID is not found.</exception>
        Task<CustomerDto> GetCustomerByIdAsync(int id);

        /// <summary>
        /// Creates a new customer based on the provided data.
        /// </summary>
        /// <param name="customerToCreate">The data for creating the new customer.</param>
        /// <returns>
        /// A <see cref="CustomerDto"/> representing the newly created customer.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="customerToCreate"/> is null.</exception>
        Task<CustomerDto> CreateCustomerAsync(CustomerForCreateDto customerToCreate);

        /// <summary>
        /// Updates an existing customer based on the provided data.
        /// </summary>
        /// <param name="customerToUpdate">The data for updating the customer.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="customerToUpdate"/> is null.</exception>
        /// <exception cref="NotFoundException">Thrown if the customer to update is not found.</exception>
        Task UpdateCustomerAsync(CustomerForUpdateDto customerToUpdate);

        /// <summary>
        /// Deletes a customer by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the customer to delete.</param>
        /// <exception cref="NotFoundException">Thrown if the customer with the specified ID is not found.</exception>
        Task DeleteCustomerAsync(int id);
    }

}