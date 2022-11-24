using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class Product : BaseAuditableEntity
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public double? Volume { get; set; }
        public double? Weight { get; set; }
        public decimal SupplyPrice { get; set; }
        public decimal SalePrice { get; set; }

        public int CategoryId { get; set; }
        public virtual ProductCategory Category { get; set; }

        public virtual ICollection<SaleDetail> SaleDetails { get; set; }
        public virtual ICollection<SupplyDetail> PurchaseDetails { get; set; }
        public virtual ICollection<InventoryDetail> InventoryProducts { get; set; }

        public Product()
        {
            SaleDetails = new List<SaleDetail>();
            PurchaseDetails = new List<SupplyDetail>();
        }
    }
}
