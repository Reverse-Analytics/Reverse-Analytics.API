using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class Supply : BaseAuditableEntity
    {
        public string? ReceivedBy { get; set; }
        public string? Comments { get; set; }
        public decimal TotalDue { get; set; }
        public decimal TotalPaid { get; set; }
        public DateTime SupplyDate { get; set; }

        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }

        public virtual ICollection<SupplyDetail> SupplyDetails { get; set; }
        public virtual ICollection<SupplyDebt> SupplyDebts { get; set; }
    }
}
