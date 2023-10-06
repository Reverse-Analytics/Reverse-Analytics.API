using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class DebtPayment : BaseAuditableEntity
    {
        public DateTime PaymentDate { get; set; }
        public string ReceivedBy { get; set; }
        public decimal Amount { get; set; }

        public int DebtId { get; set; }
        public Debt Debt { get; set; }
    }
}
