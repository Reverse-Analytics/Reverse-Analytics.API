using ReverseAnalytics.Domain.DTOs.Supply;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface ISupplyService
    {
        public Task<IEnumerable<SupplyDto>> GetAllSuppliesAsync();
        public Task<IEnumerable<SupplyDto>> GetAllSuppliesBySupplierIdAsync(int supplierId);
        public Task<SupplyDto> GetSupplyByIdAsync(int supplyId);
        public Task<SupplyDto> CreateSupplyAsync(SupplyForCreate supplyToCreate);
        public Task UpdateSupplyAsync(SupplyForUpdate supplyToUpdate);
        public Task DeleteSupplyAsync(int supplyId);
    }
}
