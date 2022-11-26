using AutoMapper;
using ReverseAnalytics.Domain.DTOs.CustomerDebt;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Exceptions;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace ReverseAnalytics.Services
{
    public class CustomerDebtService : ICustomerDebtService
    {
        private readonly ICustomerDebtRepository _repository;
        private readonly IMapper _mapper;

        public CustomerDebtService(ICustomerDebtRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerDebtDto>?> GetAllCustomerDebtsAsync()
        {
            try
            {
                var customerDebts = await _repository.FindAllAsync();

                if (customerDebts is null)
                {
                    return null;
                }

                var customerDebtDtos = _mapper.Map<IEnumerable<CustomerDebtDto>>(customerDebts);

                if (customerDebtDtos is null)
                {
                    throw new AutoMapperMappingException($"Could not map {typeof(CustomerDebt)} to type {typeof(CustomerDebtDto)}.");
                }

                return customerDebtDtos;
            }
            catch(AutoMapperMappingException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new Exception("There was an error retrieving Customer Debts.", ex);
            }
        }

        public async Task<IEnumerable<CustomerDebtDto>?> GetAllByCustomerId(int id)
        {
            try
            {
                var debts = await _repository.FindAllByCustomerId(id);

                if (debts is null)
                {
                    return null;
                }

                var debtDtos = _mapper.Map<IEnumerable<CustomerDebtDto>>(debts);

                if (debtDtos is null)
                {
                    throw new Exception($"Could not map {typeof(CustomerDebt)} to type {typeof(CustomerDebtDto)}.");
                }

                return debtDtos;
            }
            catch(AutoMapperMappingException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new Exception($"There was an error retrieving Debts for Customer with id: {id}.", ex);
            }
        }

        public async Task<CustomerDebtDto?> GetCustomerDebtByIdAsync(int id)
        {
            try
            {
                var customerDebt = await _repository.FindByIdAsync(id);

                if (customerDebt is null)
                {
                    throw new NotFoundException($"Customer Debt with id: {id} was not found.");
                }

                var customerDebtDto = _mapper.Map<CustomerDebtDto>(customerDebt);

                if(customerDebtDto is null)
                {
                    throw new AutoMapperMappingException($"Could not map {typeof(CustomerDebt)} to type {customerDebtDto}.");
                }

                return customerDebtDto;
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
                throw new Exception($"There wasn error retrieving Customer Debt with id: {id}.", ex);
            }
        }

        public async Task<CustomerDebtDto?> CreateCustomerDebtAsync(CustomerDebtForCreate customerDebtToCreate)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(customerDebtToCreate, nameof(customerDebtToCreate));

                var customerDebtEntity = _mapper.Map<CustomerDebt>(customerDebtToCreate);

                if (customerDebtEntity is null)
                {
                    throw new AutoMapperMappingException($"Could not map {typeof(CustomerDebtForCreate)} to type {typeof(CustomerDebt)}.");
                }

                var createdEntity = _repository.Create(customerDebtEntity);
                await _repository.SaveChangesAsync();

                var customerDebtDto = _mapper.Map<CustomerDebtDto>(createdEntity);

                if (customerDebtDto is null)
                {
                    throw new AutoMapperMappingException($"Could not map {typeof(CustomerDebt)} to type {typeof(CustomerDebtDto)}.");
                }

                return customerDebtDto;
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
                throw new Exception("There was an error creating new Customer Debt.", ex);
            }
        }

        public async Task UpdateCustomerDebtAsync(CustomerDebtForUpdate customerDebtToUpdate)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(customerDebtToUpdate, nameof(customerDebtToUpdate));

                var customerEntity = _mapper.Map<CustomerDebt>(customerDebtToUpdate);

                if (customerEntity is null)
                {
                    throw new AutoMapperMappingException($"Could not map {typeof(CustomerDebtForUpdate)} to type {typeof(CustomerDebt)}.");
                }

                _repository.Update(customerEntity);
                await _repository.SaveChangesAsync();
            }
            catch(ArgumentNullException ex)
            {
                throw ex;
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
                throw new Exception($"There was an error updating Customer Debt with id: {customerDebtToUpdate?.Id}.", ex);
            }
        }

        public async Task DeleteCustomerDebtAsync(int id)
        {
            try
            {
                _repository.Delete(id);
                await _repository.SaveChangesAsync();
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception($"There was an error deleting Customer Debt with id: {id}.", ex);
            }
        }
    }
}
