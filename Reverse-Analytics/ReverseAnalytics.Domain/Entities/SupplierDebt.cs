using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class SupplierDebt : BaseAuditableEntity
    {
        public decimal Amount { get; set; }
        public DateTime DebtDate { get; set; }
        public DateTime DueDate { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}
