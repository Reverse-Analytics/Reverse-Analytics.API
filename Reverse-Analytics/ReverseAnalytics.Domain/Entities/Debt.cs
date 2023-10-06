using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.Entities
{
    public class Debt : BaseAuditableEntity
    {
        public DateTime DebtDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public decimal TotalDue { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal Leftover { get; set; }
        public DebtStatus Status { get; set; }
        public Debtor Debtor { get; set; }
        public DebtOrigin Origin { get; set; }

        public int? OriginSaleId { get; set; }
        public virtual Sale? OriginSale { get; set; }

        public int? OriginRefundId { get; set; }
        public virtual Refund? OriginRefund { get; set; }

        public int? OriginDebtId { get; set; }
        public virtual Debt? OriginDebt { get; set; }

        public virtual ICollection<DebtPayment> Payments { get; set; }
    }
}
