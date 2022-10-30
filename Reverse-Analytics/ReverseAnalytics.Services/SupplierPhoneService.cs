using AutoMapper;
using ReverseAnalytics.Domain.DTOs.SupplierPhone;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Exceptions;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace ReverseAnalytics.Services
{
    public class SupplierPhoneService : ISupplierPhoneService
    {
        private readonly ICommonRepository _repository;
        private readonly IMapper _mapper;

        public SupplierPhoneService(ICommonRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<SupplierPhoneDto> CreateSupplierPhoneAsync(SupplierPhoneForCreate supplierPhoneToCreate)
        {
            try
            {
                var supplierEntity = _mapper.Map<SupplierPhone>(supplierPhoneToCreate);

                var createdSupplier = _repository.SupplierPhone.Create(supplierEntity);
                await _repository.SaveChangesAsync();

                var supplierDto = _mapper.Map<SupplierPhoneDto>(createdSupplier);

                return supplierDto;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task DeleteSupplierPhoneAsync(int id)
        {
            try
            {
                _repository.SupplierPhone.Delete(id);
                await _repository.SaveChangesAsync();
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<SupplierPhoneDto>> GetAllSupplierPhonesAsync(string? searchString)
        {
            try
            {
                var supplierPhones = await _repository.SupplierPhone.FindAllAsync();

                var supplierPhoneDtos = _mapper.Map<IEnumerable<SupplierPhoneDto>>(supplierPhones);

                return supplierPhoneDtos;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<SupplierPhoneDto> GetSupplierPhoneByIdAsync(int id)
        {
            try
            {
                var supplierPhone = await _repository.SupplierPhone.FindByIdAsync(id);

                var supplierPhoneDto = _mapper.Map<SupplierPhoneDto>(supplierPhone);

                return supplierPhoneDto;
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

        public async Task<IEnumerable<SupplierPhoneDto>> GetSupplierPhonesBySupplierIdAsync(int supplierId)
        {
            try
            {
                var supplierPhones = await _repository.SupplierPhone.FindAllBySupplierIdAsync(supplierId);

                var supplierPhoneDtos = _mapper.Map<IEnumerable<SupplierPhoneDto>>(supplierPhones);

                return supplierPhoneDtos;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateSupplierPhoneAsync(SupplierPhoneForUpdate supplierPhoneToUpdate)
        {
            try
            {
                var supplierPhoneEntity = _mapper.Map<SupplierPhone>(supplierPhoneToUpdate);

                _repository.SupplierPhone.Update(supplierPhoneEntity);
                await _repository.SaveChangesAsync();
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
