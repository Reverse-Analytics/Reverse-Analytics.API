using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class Refund : BaseAuditableEntity
    {
        public decimal TotalAmount { get; set; }
        public decimal DebtPaymentAmount { get; set; }
        public decimal RefundAmount { get; set; }
        public DateTime RefundDate { get; set; }
        public string? Reason { get; set; }
        public string? ReceivedBy { get; set; }

        public int SaleId { get; set; }
        public virtual Sale Sale { get; set; }

        public int TransactionId { get; set; }
        public virtual Transaction Transaction { get; set; }

        public ICollection<RefundItem> RefundItems { get; set; }
    }
}
