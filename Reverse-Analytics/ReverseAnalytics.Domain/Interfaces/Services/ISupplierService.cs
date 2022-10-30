using ReverseAnalytics.Domain.DTOs.Supplier;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface ISupplierService
    {
        public Task<IEnumerable<SupplierDto>> GetAllSuppliersAsync(string? searchString);
        public Task<SupplierDto> GetSupplierByIdAsync(int id);
        public Task<SupplierDto> CreateSupplierAsync(SupplierForCreateDto supplierToCreate);
        public Task UpdateSupplierAsync(SupplierForUpdateDto supplierToUpdate);
        public Task DeleteSupplierAsync(int id);
    }
}
