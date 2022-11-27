using ReverseAnalytics.Domain.DTOs.InventoryDetail;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface IInventoryDetailService
    {
        public Task<IEnumerable<InventoryDetailDto>> GetAllInventoryDetailsAsync();
        public Task<IEnumerable<InventoryDetailDto>> GetAllByInventoryIdAsync(int inventoryId);
        public Task<InventoryDetailDto> GetByInventoryAndDetailIdAsync(int inventoryId, int detailId);
        public Task<InventoryDetailDto> GetInventoryDetailByIdAsync(int id);
        public Task<InventoryDetailDto> CreateInventoryDetailAsync(InventoryDetailForCreateDto inventoryDetailToCreate);
        public Task UpdateInventoryDetailAsync(InventoryDetailForUpdateDto inventoryDetailToUpdate);
        public Task DeleteInventoryDetailAsync(int id);
    }
}
