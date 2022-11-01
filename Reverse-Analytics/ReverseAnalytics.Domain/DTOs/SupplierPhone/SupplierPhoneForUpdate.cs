namespace ReverseAnalytics.Domain.DTOs.SupplierPhone
{
    public class SupplierPhoneForUpdate
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsPrimary { get; set; }
        public int SupplierId { get; set; }
    }
}
