using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class Product : BaseAuditableEntity
    {
        public string ProductName { get; set; }
        public double Volume { get; set; }
        public double Weight { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }

        public int CategoryId { get; set; }
        public virtual ProductCategory Category { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }

        public Product()
        {
            OrderDetails = new List<OrderDetails>();
        }
    }
}
