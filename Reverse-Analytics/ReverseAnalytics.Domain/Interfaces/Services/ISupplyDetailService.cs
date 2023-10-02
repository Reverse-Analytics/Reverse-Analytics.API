using ReverseAnalytics.Domain.DTOs.SupplyItem;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface ISupplyItemservice
    {
        public Task<IEnumerable<SupplyItemDto>> GetAllSupplyItemsAsync();
        public Task<IEnumerable<SupplyItemDto>> GetAllSupplyItemsBySupplyIdAsync(int supplyId);
        public Task<IEnumerable<SupplyItemDto>> GetAllSupplyItemsByProductIdAsync(int productId);
        public Task<SupplyItemDto> GetBySupplyAndDetailIdAsync(int supplyId, int detailId);
        public Task<SupplyItemDto> GetSupplyDetailByIdAsync(int supplyDetailid);
        public Task<SupplyItemDto> CreateSupplyDetailAsync(SupplyItemForCreateDto supplyDetailToCreate);
        public Task UpdateSupplyDetailAsync(SupplyItemForUpdateDto supplyDetailToUpdate);
        public Task DeleteSupplyDetailAsync(int supplyDetailid);
    }
}
