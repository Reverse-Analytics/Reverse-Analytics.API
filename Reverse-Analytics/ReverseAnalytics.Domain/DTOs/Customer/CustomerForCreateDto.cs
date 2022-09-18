namespace ReverseAnalytics.Domain.DTOs.Customer
{
    public class CustomerForCreateDto
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Company { get; set; }
        public string? Address { get; set; }
    }
}
