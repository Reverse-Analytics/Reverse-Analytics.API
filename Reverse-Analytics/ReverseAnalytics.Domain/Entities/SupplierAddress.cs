using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class SupplierAddress : BaseEntity
    {
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public Supplier Supplier { get; set; }
        public int SupplierId { get; set; }
    }
}
