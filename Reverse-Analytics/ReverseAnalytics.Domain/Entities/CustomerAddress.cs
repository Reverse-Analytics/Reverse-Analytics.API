using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class CustomerAddress : BaseAuditableEntity
    {
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
