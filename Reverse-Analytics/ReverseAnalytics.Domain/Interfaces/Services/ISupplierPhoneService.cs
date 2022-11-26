using ReverseAnalytics.Domain.DTOs.SupplierPhone;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface ISupplierPhoneService
    {
        public Task<IEnumerable<SupplierPhoneDto>> GetAllSupplierPhonesAsync(string? searchString);
        public Task<IEnumerable<SupplierPhoneDto>> GetSupplierPhonesBySupplierIdAsync(int supplierId);
        public Task<SupplierPhoneDto> GetSupplierPhoneByIdAsync(int id);
        public Task<SupplierPhoneDto> GetSupplierPhoneBySupplierAndPhoneIdAsync(int supplierId, int phoneId);
        public Task<SupplierPhoneDto> CreateSupplierPhoneAsync(SupplierPhoneForCreate supplierPhoneToCreate);
        public Task UpdateSupplierPhoneAsync(SupplierPhoneForUpdate supplierPhoneToUpdate);
        public Task DeleteSupplierPhoneAsync(int id);
    }
}
