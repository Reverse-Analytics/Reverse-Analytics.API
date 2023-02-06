using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.Entities
{
    public class Product : BaseAuditableEntity
    {
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public UnitOfMeasurement UnitOfMeasurement { get; set; }
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
