using ReverseAnalytics.Domain.DTOs.SaleItem;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface ISaleDetailService
    {
        public Task<IEnumerable<SaleItemDto>?> GetAllSaleDetailsBySaleIdAsync(int saleId);
        public Task<SaleItemDto> GetSaleDetailBySaleAndDetailIdAsync(int saleId, int saleDetailId);
        public Task<SaleItemDto> GetSaleDetailByIdAsync(int saleDetailId);
        public Task<SaleItemDto> CreateSaleDetailAsync(SaleItemForCreateDto saleDetailToCreate);
        public Task UpdateSaleDetailAsync(SaleItemForUpdateDto saleDetailToUpdate);
        public Task DeleteSaleDetailAsync(int id);
    }
}
