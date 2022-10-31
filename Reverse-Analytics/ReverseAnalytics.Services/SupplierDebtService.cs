using AutoMapper;
using ReverseAnalytics.Domain.DTOs.SupplierDebt;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace ReverseAnalytics.Services
{
    public class SupplierDebtService : ISupplierDebtService
    {
        private readonly ICommonRepository _repository;
        private readonly IMapper _mapper;

        public SupplierDebtService(ICommonRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SupplierDebtDto>> GetAllSupplierDebtsAsync()
        {
            try
            {
                var supplierDebts = await _repository.SupplierDebt.FindAllAsync();

                var supplierDebtDtos = _mapper.Map<IEnumerable<SupplierDebtDto>>(supplierDebts);

                return supplierDebtDtos;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<SupplierDebtDto> GetSupplierDebtByIdAsync(int debtId)
        {
            try
            {
                var supplierDebt = await _repository.SupplierDebt.FindByIdAsync(debtId);

                var supplierDebtDto = _mapper.Map<SupplierDebtDto>(supplierDebt);

                return supplierDebtDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<SupplierDebtDto>> GetAllSupplierDebtsBySupplierIdAsync(int supplierId)
        {
            try
            {
                var supplierDebts = await _repository.SupplierDebt.FindAllBySupplierIdAsync(supplierId);

                var supplierDebtDtos = _mapper.Map<IEnumerable<SupplierDebtDto>>(supplierDebts);

                return supplierDebtDtos;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<SupplierDebtDto> GetSupplierDebtBySupplierAndDebtIdAsync(int supplierId, int debtId)
        {
            try
            {
                var supplierDebt = await _repository.SupplierDebt.FindBySupplierAndDebtIdAsync(supplierId, debtId);

                var supplierDebtDto = _mapper.Map<SupplierDebtDto>(supplierDebt);

                return supplierDebtDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<SupplierDebtDto> CreateSupplierDebtAsync(SupplierDebtForCreateDto supplierDebtToCreate)
        {
            try
            {
                var supplierDebtEntity = _mapper.Map<SupplierDebt>(supplierDebtToCreate);

                var createdSupplierDebt = _repository.SupplierDebt.Create(supplierDebtEntity);
                await _repository.SaveChangesAsync();

                var supplierDebtDto = _mapper.Map<SupplierDebtDto>(createdSupplierDebt);

                return supplierDebtDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateSupplierDebtAsync(SupplierDebtForUpdateDto supplierDebtToUpdate)
        {
            try
            {
                var supplierDebtEntity = _mapper.Map<SupplierDebt>(supplierDebtToUpdate);

                _repository.SupplierDebt.Update(supplierDebtEntity);
                await _repository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public async Task DeleteSupplierDebtAsync(int debtId)
        {
            try
            {
                _repository.SupplierDebt.Delete(debtId);
                await _repository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
