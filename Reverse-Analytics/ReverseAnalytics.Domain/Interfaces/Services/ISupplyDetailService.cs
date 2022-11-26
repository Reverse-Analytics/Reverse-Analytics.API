using ReverseAnalytics.Domain.DTOs.SupplyDetail;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface ISupplyDetailService
    {
        public Task<IEnumerable<SupplyDetailDto>> GetAllSupplyDetailsAsync();
        public Task<IEnumerable<SupplyDetailDto>> GetAllSupplyDetailsBySupplyIdAsync(int supplyId);
        public Task<IEnumerable<SupplyDetailDto>> GetAllSupplyDetailsByProductIdAsync(int productId);
        public Task<SupplyDetailDto> GetBySupplyAndDetailIdAsync(int supplyId, int detailId);
        public Task<SupplyDetailDto> GetSupplyDetailByIdAsync(int supplyDetailid);
        public Task<SupplyDetailDto> CreateSupplyDetailAsync(SupplyDetailForCreateDto supplyDetailToCreate);
        public Task UpdateSupplyDetailAsync(SupplyDetailForUpdateDto supplyDetailToUpdate);
        public Task DeleteSupplyDetailAsync(int supplyDetailid);
    }
}
