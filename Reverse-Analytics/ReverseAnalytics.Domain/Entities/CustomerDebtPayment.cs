using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class CustomerDebtPayment : BaseAuditableEntity
    {
        public int DebtId { get; set; }
        public CustomerDebt Debt { get; set; }

        public int TransactionId { get; set; }
        public virtual Transaction Transaction { get; set; }
    }
}
