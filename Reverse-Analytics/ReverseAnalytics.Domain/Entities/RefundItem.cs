using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class RefundItem : BaseAuditableEntity
    {
        public int Quantity { get; set; }

        public int RefundId { get; set; }
        public virtual Refund Refund { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
