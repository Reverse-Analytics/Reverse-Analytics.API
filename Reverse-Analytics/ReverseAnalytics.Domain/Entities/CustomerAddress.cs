using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class CustomerAddress : BaseAuditableEntity
    {
        public string AddressDetails { get; set; }

        public int? CityId { get; set; }
        public City? City { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
