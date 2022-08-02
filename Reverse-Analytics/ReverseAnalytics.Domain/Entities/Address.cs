namespace ReverseAnalytics.Domain.Entities
{
    public class Address
    {
        public int AddressId { get; set; }
        public string AddressDetails { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

        public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; }
    }
}
