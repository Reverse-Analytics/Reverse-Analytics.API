namespace ReverseAnalytics.Domain.DTOs.SupplierPhone
{
    public class SupplierPhoneForCreate
    {
        public string PhoneNumber { get; set; }
        public int SupplierId { get; set; }
        public bool IsPrimary { get; set; }
    }
}
