using AutoMapper;
using ReverseAnalytics.Domain.DTOs.CustomerPhone;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Exceptions;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace ReverseAnalytics.Services
{
    public class CustomerPhoneService : ICustomerPhoneService
    {
        private readonly IMapper _mapper;
        private readonly ICommonRepository _repository;

        public CustomerPhoneService(IMapper mapper, ICommonRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<CustomerPhoneDto>?> GetCustomerPhonesAsync()
        {
            try
            {
                var customerPhones = await _repository.CustomerPhone.FindAllAsync();

                if(customerPhones is null)
                {
                    return null;
                }

                var customerPhoneDtos = _mapper.Map<IEnumerable<CustomerPhoneDto>>(customerPhones);

                if(customerPhoneDtos is null)
                {
                    throw new AutoMapperMappingException($"Could not map {typeof(CustomerPhone)} entities to type {typeof(CustomerPhoneDto)}.");
                }

                return customerPhoneDtos;
            }
            catch (AutoMapperMappingException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error retrieving Customer Phones.", ex);
            }
        }
        
        public async Task<IEnumerable<CustomerPhoneDto>?> GetCustomerPhonesByCustomerIdAsync(int customerId)
        {
            try
            {
                var customerPhones = await _repository.CustomerPhone.FindAllByCustomerId(customerId);

                if(customerPhones is null)
                {
                    return null;
                }

                var customerPhoneDtos = _mapper.Map<IEnumerable<CustomerPhoneDto>>(customerPhones);

                if(customerPhoneDtos is null)
                {
                    throw new AutoMapperMappingException($"There was an error mapping {typeof(CustomerPhone)} entities to type {typeof(CustomerPhoneDto)}.");
                }

                return customerPhoneDtos;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
            catch (AutoMapperMappingException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new Exception($"There was an error retrieving Customer Phones with Customer id: {customerId}.", ex);
            }

        }
        
        public async Task<CustomerPhoneDto?> GetCustomerPhoneByCustomerAndPhoneIdAsync(int customerId, int phoneId)
        {
            try
            {
                var customerPhone = await _repository.CustomerPhone.FindByCustomerAndPhoneIdAsync(customerId, phoneId);

                if (customerPhone is null)
                {
                    return null;
                }

                var customerPhoneDto = _mapper.Map<CustomerPhoneDto>(customerPhone);

                if (customerPhoneDto is null)
                {
                    throw new AutoMapperMappingException($"Could not map {typeof(CustomerPhone)} to type {typeof(CustomerPhoneDto)}");
                }

                return customerPhoneDto;
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
                throw new Exception($"There was an error retrieving Customer Phone with Customer id: {customerId}, and Phone id: {phoneId}.", ex);
            }
        }

        public async Task<CustomerPhoneDto> CreateCustomerPhoneAsync(CustomerPhoneForCreate customerPhoneToCreate)
        {
            try
            {
                if (customerPhoneToCreate is null)
                {
                    throw new ArgumentNullException(nameof(customerPhoneToCreate));
                }

                var customerPhoneEntity = _mapper.Map<CustomerPhone>(customerPhoneToCreate);

                if (customerPhoneEntity is null)
                {
                    throw new AutoMapperMappingException($"Could not map {typeof(CustomerPhoneForCreate)} to {typeof(CustomerPhone)}.");
                }

                var createdEntity = _repository.CustomerPhone.Create(customerPhoneEntity);
                await _repository.CustomerPhone.SaveChangesAsync();

                var customerPhoneDto = _mapper.Map<CustomerPhoneDto>(createdEntity);

                if (customerPhoneDto is null)
                {
                    throw new AutoMapperMappingException($"Could not map {typeof(CustomerPhone)} to {typeof(CustomerPhoneDto)}.");
                }

                return customerPhoneDto;
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
                throw new Exception("There was an error adding new Customer Phone.", ex);
            }
        }

        public async Task UpdateCustomerPhoneAsync(CustomerPhoneForUpdate customerPhoneToUpdate)
        {
            try
            {
                if (customerPhoneToUpdate is null)
                {
                    throw new ArgumentNullException(nameof(customerPhoneToUpdate));
                }

                var customerEntity = _mapper.Map<CustomerPhone>(customerPhoneToUpdate);

                if (customerEntity is null)
                {
                    throw new AutoMapperMappingException($"Could not map {typeof(CustomerPhoneForUpdate)} to type {typeof(CustomerPhone)}.");
                }

                _repository.CustomerPhone.Update(customerEntity);
                await _repository.CustomerPhone.SaveChangesAsync();
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
                throw new Exception("There was an error updating Customer.", ex);
            }
        }
        
        public async Task DeleteCustomerPhoneAsync(int phoneId)
        {
            try
            {
                _repository.CustomerPhone.Delete(phoneId);
                await _repository.CustomerPhone.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"There was an error deleting Customer Phone with id: {phoneId}.", ex);
            }
        }
    }
}
