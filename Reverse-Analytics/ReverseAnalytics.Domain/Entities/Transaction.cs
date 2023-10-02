using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.Entities
{
    public class Transaction : BaseAuditableEntity
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string? Comments { get; set; }
        public TransactionType Type { get; set; }
    }
}
