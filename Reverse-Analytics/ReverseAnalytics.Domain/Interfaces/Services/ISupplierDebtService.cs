using ReverseAnalytics.Domain.DTOs.SupplierDebt;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface ISupplierDebtService
    {
        public Task<IEnumerable<SupplierDebtDto>> GetAllSupplierDebtsAsync();
        public Task<IEnumerable<SupplierDebtDto>> GetAllSupplierDebtsBySupplierIdAsync(int supplierId);
        public Task<SupplierDebtDto> GetSupplierDebtByIdAsync(int debtId);
        public Task<SupplierDebtDto> GetSupplierDebtBySupplierAndDebtIdAsync(int supplierId, int debtId);
        public Task<SupplierDebtDto> CreateSupplierDebtAsync(SupplierDebtForCreateDto supplierDebtToCreate);
        public Task UpdateSupplierDebtAsync(SupplierDebtForUpdateDto supplierDebtToUpdate);
        public Task DeleteSupplierDebtAsync(int debtId);
    }
}
