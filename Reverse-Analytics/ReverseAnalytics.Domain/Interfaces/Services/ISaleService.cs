using ReverseAnalytics.Domain.DTOs.Sale;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface ISaleService
    {
        public Task<IEnumerable<SaleDto>> GetAllSalesAsync(int pageSize, int pageNumber);
        public Task<IEnumerable<SaleDto>> GetAllByCustomerIdAsync(int customerId);
        public Task<SaleDto> GetSaleByIdAsync(int id);
        public Task<SaleDto> GetByCustomerAndSaleIdAsync(int customerId, int saleId);
        public Task<SaleDto> CreateSaleAsync(SaleForCreateDto SaleToCreate);
        public Task UpdateSaleAsync(SaleForUpdateDto SaleToUpdate);
        public Task DeleteSaleAsync(int id);
    }
}
