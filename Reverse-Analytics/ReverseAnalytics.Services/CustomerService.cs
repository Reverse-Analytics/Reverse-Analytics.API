using AutoMapper;
using ReverseAnalytics.Domain.DTOs.Customer;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Exceptions;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace ReverseAnalytics.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICommonRepository _repository;
        private readonly IMapper _mapper;

        public CustomerService(ICommonRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<CustomerDto>> GetAllCustomerAsync(string? searchString, int pageNumber, int pageSize)
        {
            var customers = await _repository.Customer.FindAllCustomers(searchString, pageNumber, pageSize);

            return customers.Any() ? _mapper.Map<IEnumerable<CustomerDto>>(customers) : Enumerable.Empty<CustomerDto>();
        }

        /// <inheritdoc />
        public async Task<CustomerDto> GetCustomerByIdAsync(int id)
        {
            var customer = await _repository.Customer.FindByIdAsync(id) ?? throw new NotFoundException($"Customer with id {id} was not found.");
            var customerDto = _mapper.Map<CustomerDto>(customer);

            return customerDto is null
                ? throw new AutoMapperMappingException($"Could not map {typeof(Customer)} to type {typeof(CustomerDto)}")
                : customerDto;
        }

        /// <inheritdoc />
        public async Task<CustomerDto> CreateCustomerAsync(CustomerForCreateDto customerToCreate)
        {
            ValidateNotNull(customerToCreate);

            var customerEntity = _mapper.Map<Customer>(customerToCreate) ??
                throw new AutoMapperMappingException($"Could not map {typeof(CustomerForCreateDto)} to {typeof(Customer)}.");

            var createdEntity = _repository.Customer.Create(customerEntity);
            await _repository.Customer.SaveChangesAsync();

            var customerDto = _mapper.Map<CustomerDto>(createdEntity);

            return customerDto is null
                ? throw new AutoMapperMappingException($"Could not map {typeof(Customer)} to type {typeof(CustomerDto)}")
                : customerDto;
        }

        /// <inheritdoc />
        public async Task UpdateCustomerAsync(CustomerForUpdateDto customerToUpdate)
        {
            ValidateNotNull(customerToUpdate);

            var customerEntity = _mapper.Map<Customer>(customerToUpdate) ??
                throw new AutoMapperMappingException($"Could not map {typeof(CustomerForUpdateDto)} to type {typeof(Customer)}.");

            _repository.Customer.Update(customerEntity);
            await _repository.Customer.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task DeleteCustomerAsync(int id)
        {
            _repository.Customer.Delete(id);
            await _repository.Customer.SaveChangesAsync();
        }

        private static void ValidateNotNull<T>(T entity) => ArgumentNullException.ThrowIfNull(entity);
    }
}
