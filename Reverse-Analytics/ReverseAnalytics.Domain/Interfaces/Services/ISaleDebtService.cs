using ReverseAnalytics.Domain.DTOs.SaleDebt;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface ISaleDebtService
    {
        public Task<IEnumerable<SaleDebtDto>> GetAllSaleDebtsAsync();
        public Task<IEnumerable<SaleDebtDto>> GetSaleDebtsBySaleIdAsync(int saleId);
        public Task<SaleDebtDto> GetSaleDebtByIdAsync(int id);
        public Task<SaleDebtDto> CreateSaleDebtAsync(SaleDebtForCreateDto saleDebtToCreate);
        public Task UpdateSaleDebtAsync(SaleDebtForUpdateDto saleDebtToUpdate);
        public Task DeleteSaleDebtAsync(int id);
        public Task<SaleDebtDto> SettleDebtAsync(int id);
        public Task<SaleDebtDto> MakePaymentAsync(int id, decimal amount);
    }
}
