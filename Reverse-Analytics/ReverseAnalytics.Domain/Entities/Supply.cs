using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class Supply : BaseAuditableEntity
    {
        public DateTime? PurchaseDate { get; set; }
        public decimal TotalDue { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal? DebtAmount { get; set; }
        public string? ReceivedBy { get; set; }

        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }

        public virtual ICollection<SupplyDetail> SupplyDetails { get; set; }

        public Supply()
        {
            SupplyDetails = new List<SupplyDetail>();
        }
    }
}
