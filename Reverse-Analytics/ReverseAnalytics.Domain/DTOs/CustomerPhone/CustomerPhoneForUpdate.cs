namespace ReverseAnalytics.Domain.DTOs.CustomerPhone
{
    public class CustomerPhoneForUpdate
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsPrimary { get; set; }

        public int CustomerId { get; set; }
    }
}
