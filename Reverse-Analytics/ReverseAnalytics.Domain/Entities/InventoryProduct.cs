using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class InventoryProduct : BaseEntity
    {
        public double ProductsRemained { get; set; }
        public double EnoughForDays { get; set; }

        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
