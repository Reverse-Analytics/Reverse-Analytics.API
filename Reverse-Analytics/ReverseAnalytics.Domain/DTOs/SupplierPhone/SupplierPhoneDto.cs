using ReverseAnalytics.Domain.DTOs.Supplier;

namespace ReverseAnalytics.Domain.DTOs.SupplierPhone
{
    public class SupplierPhoneDto
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsPrimary { get; set; }

        public int SupplierId { get; set; }
        public SupplierDto Supplier { get; set; }
    }
}
