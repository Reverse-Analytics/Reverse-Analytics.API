namespace ReverseAnalytics.Domain.DTOs.CustomerPhoneDto
{
    public class CustomerForUpdateDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string? CompanyName { get; set; }
        public decimal? Balance { get; set; }
        public bool IsActive { get; set; }
        public string? ContactPerson { get; set; }
        public string? ContactPersonPhone { get; set; }
    }
}
