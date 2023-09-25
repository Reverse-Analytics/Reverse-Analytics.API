using ReverseAnalytics.Domain.DTOs.SupplyItem;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface ISupplyDetailService
    {
        public Task<IEnumerable<SupplyItemDto>> GetAllSupplyDetailsAsync();
        public Task<IEnumerable<SupplyItemDto>> GetAllSupplyDetailsBySupplyIdAsync(int supplyId);
        public Task<IEnumerable<SupplyItemDto>> GetAllSupplyDetailsByProductIdAsync(int productId);
        public Task<SupplyItemDto> GetBySupplyAndDetailIdAsync(int supplyId, int detailId);
        public Task<SupplyItemDto> GetSupplyDetailByIdAsync(int supplyDetailid);
        public Task<SupplyItemDto> CreateSupplyDetailAsync(SupplyItemForCreateDto supplyDetailToCreate);
        public Task UpdateSupplyDetailAsync(SupplyItemForUpdateDto supplyDetailToUpdate);
        public Task DeleteSupplyDetailAsync(int supplyDetailid);
    }
}
