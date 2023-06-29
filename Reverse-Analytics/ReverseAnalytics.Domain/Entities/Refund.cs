using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class Refund : BaseAuditableEntity
    {
        public DateTime RefundDate { get; set; }
        public string? Reason { get; set; }
        public string? ReceivedBy { get; set; }

        public ICollection<RefundDetail> RefundDetails { get; set; }
    }
}
