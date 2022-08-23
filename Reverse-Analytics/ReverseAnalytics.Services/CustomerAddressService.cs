using AutoMapper;
using ReverseAnalytics.Domain.DTOs.CustomerAddress;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Exceptions;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace ReverseAnalytics.Services
{
    public class CustomerAddressService : ICustomerAddressService
    {
        private readonly ICommonRepository _repository;
        private readonly IMapper _mapper;

        public CustomerAddressService(ICommonRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerAddressDto>?> GetCustomerAddressesAsync()
        {
            try
            {
                var customerAddresses = await _repository.CustomerAddress.FindAllAsync();

                if (customerAddresses is null)
                {
                    return null;
                }

                var customerAddressDtos = _mapper.Map<IEnumerable<CustomerAddressDto>?>(customerAddresses);

                if (customerAddressDtos is null)
                {
                    throw new AutoMapperMappingException($"Could not map {typeof(CustomerAddress)} entities to type {typeof(CustomerAddressDto)}.");
                }

                return customerAddressDtos;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
            catch (AutoMapperMappingException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error retrieving Customer addresses.", ex);
            }
        }

        public async Task<CustomerAddressDto?> GetCustomerAddressByIdAsync(int id)
        {
            try
            {
                var customerAddress = await _repository.CustomerAddress.FindByIdAsync(id);

                if (customerAddress is null)
                {
                    return null;
                }

                var customerAddressDto = _mapper.Map<CustomerAddressDto>(customerAddress);

                if (customerAddressDto is null)
                {
                    throw new AutoMapperMappingException($"Could not map {typeof(CustomerAddress)} to type {typeof(CustomerAddressDto)}");
                }

                return customerAddressDto;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
            catch (AutoMapperMappingException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception($"There was an error retrieving customer address with id: {id}.", ex);
            }
        }

        public async Task<CustomerAddressDto?> CreateCustomerAddressAsync(CustomerAddressForCreateDto customerAddressToCreate)
        {
            try
            {
                if (customerAddressToCreate is null)
                {
                    throw new ArgumentNullException(nameof(customerAddressToCreate));
                }

                var customerAddressEntity = _mapper.Map<CustomerAddress>(customerAddressToCreate);

                if (customerAddressEntity is null)
                {
                    throw new AutoMapperMappingException($"Could not map {typeof(CustomerAddressForCreateDto)} to {typeof(CustomerAddress)}.");
                }

                var createdEntity = _repository.CustomerAddress.Create(customerAddressEntity);
                await _repository.Customer.SaveChangesAsync();

                var customerAddressDto = _mapper.Map<CustomerAddressDto>(createdEntity);

                if (customerAddressDto is null)
                {
                    throw new AutoMapperMappingException($"Could not map {typeof(CustomerAddress)} to {typeof(CustomerAddressDto)}.");
                }

                return customerAddressDto;
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (AutoMapperMappingException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error adding new Customer.", ex);
            }
        }

        public async Task DeleteCustomerAddressAsync(int id)
        {
            try
            {
                _repository.CustomerAddress.Delete(id);
                await _repository.CustomerAddress.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"There was an error deleting Customer address with id: {id}.", ex);
            }
        }

        public async Task UpdateCustomerAddressAsync(CustomerAddressForUpdateDto customerAddressToUpdate)
        {
            try
            {
                if (customerAddressToUpdate is null)
                {
                    throw new ArgumentNullException(nameof(customerAddressToUpdate));
                }

                var customerAddressEntity = _mapper.Map<CustomerAddress>(customerAddressToUpdate);

                if (customerAddressEntity is null)
                {
                    throw new AutoMapperMappingException($"Could not map {typeof(CustomerAddressForUpdateDto)} to type {typeof(CustomerAddress)}.");
                }

                _repository.CustomerAddress.Update(customerAddressEntity);
                await _repository.CustomerAddress.SaveChangesAsync();
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (AutoMapperMappingException ex)
            {
                throw ex;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error updating Customer address.", ex);
            }
        }
    }
}
