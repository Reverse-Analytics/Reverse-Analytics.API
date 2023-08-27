namespace ReverseAnalytics.Domain.DTOs.Customer
{
    public class CustomerForCreateDto
    {
        public string FullName { get; set; }
        public string? Company { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public decimal Balance { get; set; }
        public double Discount { get; set; }
        public bool IsActive { get; set; }
    }
}
