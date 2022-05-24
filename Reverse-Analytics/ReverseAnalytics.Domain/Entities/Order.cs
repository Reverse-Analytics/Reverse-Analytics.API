using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.Entities
{
    public class Order : BaseAuditableEntity
    {
        public decimal TotalDue { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? DiscountTotal { get; set; }
        public string? Comment { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus? Status { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }

        public Order()
        {
            OrderDetails = new List<OrderDetails>();
        }
    }
}
