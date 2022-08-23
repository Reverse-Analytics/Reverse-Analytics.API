using AutoMapper;
using ReverseAnalytics.Domain.DTOs.Customer;
using ReverseAnalytics.Domain.DTOs.CustomerAddress;
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

        public async Task<IEnumerable<CustomerDto>?> GetAllCustomerAsync(string? searchString)
        {
            try
            {
                var customers = await _repository.Customer.FindAllCustomers(searchString);

                if(customers is null)
                {
                    return null;
                }

                var customerDtos = _mapper.Map<IEnumerable<CustomerDto>?>(customers);

                if(customerDtos is null)
                {
                    throw new AutoMapperMappingException($"Could not map {typeof(Customer)} entities to type {typeof(CustomerDto)}.");
                }

                return customerDtos;
            }
            catch(NotFoundException ex)
            {
                throw ex;
            }
            catch(AutoMapperMappingException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new Exception("There was an error retrieving Customers.", ex);
            }
        }

        public async Task<CustomerDto?> GetCustomerByIdAsync(int id)
        {
            try
            {
                var customer = await _repository.Customer.FindByIdAsync(id);

                if(customer is null)
                {
                    return null;
                }

                var customerDto = _mapper.Map<CustomerDto>(customer);

                if(customerDto is null)
                {
                    throw new AutoMapperMappingException($"Could not map {typeof(Customer)} to type {typeof(CustomerDto)}");
                }

                return customerDto;
            }
            catch(NotFoundException ex)
            {
                throw ex;
            }
            catch(AutoMapperMappingException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new Exception($"There was an error retrieving customer with id: {id}.", ex);
            }
        }

        public async Task<CustomerDto?> CreateCustomerAsync(CustomerForCreateDto customerToCreate)
        {
            try
            {
                if (customerToCreate is null)
                {
                    throw new ArgumentNullException(nameof(customerToCreate));
                }

                var customerEntity = _mapper.Map<Customer>(customerToCreate);

                if (customerEntity is null)
                {
                    throw new AutoMapperMappingException($"Could not map {typeof(CustomerForCreateDto)} to {typeof(Customer)}.");
                }

                var createdEntity = _repository.Customer.Create(customerEntity);
                await _repository.Customer.SaveChangesAsync();

                var customerDto = _mapper.Map<CustomerDto>(createdEntity);

                if(customerDto is null)
                {
                    throw new AutoMapperMappingException($"Could not map {typeof(Customer)} to {typeof(CustomerDto)}.");
                }

                return customerDto;
            }
            catch (ArgumentNullException ex) 
            {
                throw ex;
            }
            catch(AutoMapperMappingException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new Exception("There was an error adding new Customer.", ex);
            }
        }

        public async Task DeleteCustomerAsync(int id)
        {
            try
            {
                _repository.Customer.Delete(id);
                await _repository.Customer.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception($"There was an error deleting Customer with id: {id}", ex);
            }
        }

        public async Task UpdateCustomerAsync(CustomerForUpdateDto customerToUpdate)
        {
            try
            {
                if(customerToUpdate is null)
                {
                    throw new ArgumentNullException(nameof(customerToUpdate));
                }

                var customerEntity = _mapper.Map<Customer>(customerToUpdate);

                if(customerEntity is null)
                {
                    throw new AutoMapperMappingException($"Could not map {typeof(CustomerForUpdateDto)} to type {typeof(Customer)}.");
                }

                _repository.Customer.Update(customerEntity);
                await _repository.Customer.SaveChangesAsync();
            }
            catch(ArgumentNullException ex)
            {
                throw ex;
            }
            catch(AutoMapperMappingException ex)
            {
                throw ex;
            }
            catch(NotFoundException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new Exception("There was an error updating Customer.", ex);
            }
        }
    }
}
