using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class CustomerDebt : BaseAuditableEntity
    {
        public decimal Amount { get; set; }
        public DateTime DebtDate { get; set; }
        public DateTime DueDate { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
