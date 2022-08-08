using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class Address : BaseAuditableEntity
    {
        public string AddressDetails { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

        public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; }
    }
}
