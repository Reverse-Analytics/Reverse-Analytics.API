namespace ReverseAnalytics.Domain.DTOs.Phone
{
    public class PhoneDto
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsPrimary { get; set; }

        public int PersonId { get; set; }
    }
}
