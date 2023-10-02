using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.Entities
{
    public class CustomerDebt : BaseAuditableEntity
    {
        public DateTime DebtDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal TotalDue { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal Leftover { get; set; }
        public DebtStatus Status { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public int? SaleId { get; set; }
        public virtual Sale? Sale { get; set; }

        public int? RefundId { get; set; }
        public virtual Refund? Refund { get; set; }

        public int? CustomerDebtId { get; set; }
        public virtual CustomerDebt? Debt { get; set; }

        public virtual IEnumerable<CustomerDebtPayment> DebtPayments { get; set; }
    }
}
