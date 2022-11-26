using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class Debt : BaseAuditableEntity
    {
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
