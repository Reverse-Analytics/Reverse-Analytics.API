namespace ReverseAnalytics.Domain.DTOs.Supplier
{
    public class SupplierForCreateDto
    {
        public string FullName { get; set; }
        public string? CompanyName { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
    }
}
