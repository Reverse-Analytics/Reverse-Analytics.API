using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.Entities
{
    public class Transaction : BaseAuditableEntity
    {
        public decimal TotalDue { get; set; }
        public decimal TotalPaid { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? Comments { get; set; }
        public TransactionStatusType Status { get; set; }

        public virtual Debt Debt { get; set; }
    }
}
