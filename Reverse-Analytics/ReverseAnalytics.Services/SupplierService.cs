using AutoMapper;
using ReverseAnalytics.Domain.DTOs.Supplier;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Exceptions;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace ReverseAnalytics.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ICommonRepository _repository;
        private readonly IMapper _mapper;

        public SupplierService(ICommonRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SupplierDto>> GetAllSuppliersAsync(string? searchString)
        {
            try
            {
                var suppliers = await _repository.Supplier.FindAllAsync();

                var supplierDtos = _mapper.Map<IEnumerable<SupplierDto>>(suppliers);

                return supplierDtos;
            }
            catch (AutoMapperMappingException)
            {
                throw;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<SupplierDto> GetSupplierByIdAsync(int id)
        {
            try
            {
                var supplier = await _repository.Supplier.FindByIdAsync(id);

                var supplierDto = _mapper.Map<SupplierDto>(supplier);

                return supplierDto;
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (AutoMapperMappingException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public async Task<SupplierDto> CreateSupplierAsync(SupplierForCreateDto supplierToCreate)
        {
            try
            {
                var supplierEntity = _mapper.Map<Supplier>(supplierToCreate);

                if(supplierEntity is null)
                {
                    throw new AutoMapperMappingException($"There was an error mapping {typeof(SupplierForCreateDto)} to type {typeof(Supplier)}.");
                }

                var createdEntity = _repository.Supplier.Create(supplierEntity);
                await _repository.Supplier.SaveChangesAsync();

                var supplierDto = _mapper.Map<SupplierDto>(createdEntity);

                return supplierDto;
            }
            catch (AutoMapperMappingException)
            {
                throw;
            }
            catch(Exception)
            {
                throw;
            }
        }
        
        public async Task UpdateSupplierAsync(SupplierForUpdateDto supplierToUpdate)
        {
            try
            {
                var supplierEntity = _mapper.Map<Supplier>(supplierToUpdate);

                _repository.Supplier.Update(supplierEntity);
                await _repository.Supplier.SaveChangesAsync();
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (AutoMapperMappingException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteSupplierAsync(int id)
        {
            try
            {
                _repository.Supplier.Delete(id);
                await _repository.Supplier.SaveChangesAsync();
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
