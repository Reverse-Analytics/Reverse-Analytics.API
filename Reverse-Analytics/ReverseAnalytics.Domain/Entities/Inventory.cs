using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class Inventory : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<InventoryProduct> Products { get; set; }

        public Inventory(string name)
        {
            Name = name;

            Products = new List<InventoryProduct>();
        }
    }
}
