using ReverseAnalytics.Domain.DTOs.SaleDetail;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface ISaleDetailService
    {
        public Task<IEnumerable<SaleDetailDto>?> GetAllSaleDetailsBySaleIdAsync(int saleId);
        public Task<SaleDetailDto> GetSaleDetailBySaleAndDetailIdAsync(int saleId, int saleDetailId);
        public Task<SaleDetailDto> GetSaleDetailByIdAsync(int saleDetailId);
        public Task<SaleDetailDto> CreateSaleDetailAsync(SaleDetailForCreateDto saleDetailToCreate);
        public Task UpdateSaleDetailAsync(SaleDetailForUpdateDto saleDetailToUpdate);
        public Task DeleteSaleDetailAsync(int id);
    }
}
