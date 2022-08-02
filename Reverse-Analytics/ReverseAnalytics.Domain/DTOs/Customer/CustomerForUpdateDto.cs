namespace ReverseAnalytics.Domain.DTOs.Customer
{
    public class CustomerForUpdateDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Company { get; set; }
    }
}
