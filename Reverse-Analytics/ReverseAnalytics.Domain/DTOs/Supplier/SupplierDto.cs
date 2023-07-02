namespace ReverseAnalytics.Domain.DTOs.Supplier
{
    public class SupplierDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
    }
}
