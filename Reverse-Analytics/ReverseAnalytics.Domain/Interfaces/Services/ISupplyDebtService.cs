using ReverseAnalytics.Domain.DTOs.SupplyDebt;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface ISupplyDebtService
    {
        public Task<IEnumerable<SupplyDebtDto>> GetAllSupplyDebtsAsync();
        public Task<IEnumerable<SupplyDebtDto>> GetSupplyDebtsBySupplyIdAsync(int supplyId);
        public Task<SupplyDebtDto> GetSupplyDebtByIdAsync(int id);
        public Task<SupplyDebtDto> CreateSupplyDebtAsync(SupplyDebtForCreateDto supplyDebtToCreate);
        public Task UpdateSupplyDebtAsync(SupplyDebtForUpdateDto supplyDebtToUpdate);
        public Task DeleteSupplyDebtAsync(int id);
    }
}
