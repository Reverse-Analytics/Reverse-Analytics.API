using AutoMapper;
using ReverseAnalytics.Domain.DTOs.SupplyDebt;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace ReverseAnalytics.Services
{
    public class SupplyDebtService : ISupplyDebtService
    {
        private readonly ICommonRepository _repository;
        private readonly IMapper _mapper;

        public SupplyDebtService(ICommonRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SupplyDebtDto> CreateSupplyDebtAsync(SupplyDebtForCreateDto supplyDebtToCreate)
        {
            ArgumentNullException.ThrowIfNull(supplyDebtToCreate);

            var SupplyDebtEntity = _mapper.Map<SupplyDebt>(supplyDebtToCreate);

            _repository.SupplyDebt.Create(SupplyDebtEntity);
            await _repository.SaveChangesAsync();

            return _mapper.Map<SupplyDebtDto>(SupplyDebtEntity);
        }

        public async Task DeleteSupplyDebtAsync(int id)
        {
            _repository.SupplyDebt.Delete(id);
            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<SupplyDebtDto>> GetAllSupplyDebtsAsync()
        {
            var supplyDebts = await _repository.SupplyDebt.FindAllAsync();

            return supplyDebts is null ?
                Enumerable.Empty<SupplyDebtDto>() :
                _mapper.Map<IEnumerable<SupplyDebtDto>>(supplyDebts);
        }

        public async Task<SupplyDebtDto> GetSupplyDebtByIdAsync(int id)
        {
            var supplyDebt = await _repository.SupplyDebt.FindByIdAsync(id);

            return _mapper.Map<SupplyDebtDto>(supplyDebt);
        }

        public async Task<IEnumerable<SupplyDebtDto>> GetSupplyDebtsBySupplyIdAsync(int supplyId)
        {
            var SupplyDebts = await _repository.SupplyDebt.FindAllBySupplyIdAsync(supplyId);

            return SupplyDebts is null ?
                Enumerable.Empty<SupplyDebtDto>() :
                _mapper.Map<IEnumerable<SupplyDebtDto>>(SupplyDebts);
        }

        public async Task UpdateSupplyDebtAsync(SupplyDebtForUpdateDto supplyDebtToUpdate)
        {
            ArgumentNullException.ThrowIfNull(supplyDebtToUpdate);

            var supplyDebtEntity = _mapper.Map<SupplyDebt>(supplyDebtToUpdate);

            _repository.SupplyDebt.Update(supplyDebtEntity);
            await _repository.SaveChangesAsync();

        }
    }
}
