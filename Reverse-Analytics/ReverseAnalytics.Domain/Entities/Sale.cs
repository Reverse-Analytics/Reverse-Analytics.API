using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.Entities
{
    public class Sale : Transaction
    {
        public string Receipt { get; set; }
        public decimal Discount { get; set; }
        public SaleType SaleType { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual ICollection<SaleDetail> OrderDetails { get; set; }

        public Sale()
        {
            OrderDetails = new List<SaleDetail>();
        }
    }
}
