using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.Entities
{
    public class SaleDebt : BaseAuditableEntity
    {
        public decimal TotalDue { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public DebtStatus Status { get; set; }

        public int SaleId { get; set; }
        public virtual Sale Sale { get; set; }
    }
}
