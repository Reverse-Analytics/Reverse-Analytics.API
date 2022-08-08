using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class SupplierPhone : BaseAuditableEntity
    {
        public string PhoneNumber { get; set; }
        public bool IsPrimary { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}
