using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class Purchase : BaseAuditableEntity
    {
        public DateTime? PurchaseDate { get; set; }
        public decimal TotalDue { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal? Debt { get; set; }
        public string? ReceivedBy { get; set; }

        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }

        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }

        public Purchase()
        {
            PurchaseDetails = new List<PurchaseDetail>();
        }
    }
}
