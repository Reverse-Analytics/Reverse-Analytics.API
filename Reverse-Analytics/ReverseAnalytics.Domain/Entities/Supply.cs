using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.Entities
{
    public class Supply : BaseAuditableEntity
    {
        public string? ReceivedBy { get; set; }
        public string? Comment { get; set; }
        public DateTime SupplyDate { get; set; }
        public decimal TotalDue { get; set; }
        public decimal TotalPaid { get; set; }
        public TransactionStatus Status { get; set; }

        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }

        public virtual ICollection<SupplyDetail> SupplyDetails { get; set; }

        public Supply()
        {
            SupplyDetails = new List<SupplyDetail>();
        }
    }
}
