namespace ReverseAnalytics.Domain.DTOs.CustomerPhone
{
    public class CustomerPhoneForCreate
    {
        public string PhoneNumber { get; set; }
        public bool IsPrimary { get; set; }

        public int CustomerId { get; set; }
    }
}
