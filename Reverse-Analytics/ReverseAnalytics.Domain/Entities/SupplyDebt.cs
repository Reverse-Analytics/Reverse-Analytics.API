using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.Entities
{
    public class SupplyDebt : BaseAuditableEntity
    {
        public decimal TotalDue { get; set; }
        public decimal TotalPaid { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public DebtStatus Status { get; set; }

        public int SupplyId { get; set; }
        public virtual Supply Supply { get; set; }
    }
}
