namespace ReverseAnalytics.Domain.Entities
{
    public class CustomerPhone
    {
        public int CustomerPhoneId { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsPrimary { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
