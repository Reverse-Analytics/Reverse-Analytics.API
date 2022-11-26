namespace ReverseAnalytics.Domain.DTOs.Phone
{
    public class PhoneForCreateDto
    {
        public string PhoneNumber { get; set; }
        public bool IsPrimary { get; set; }

        public int PersonId { get; set; }
    }
}
