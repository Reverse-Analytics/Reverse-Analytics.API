using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class Inventory : BaseAuditableEntity
    {
        public string Name { get; set; }

        public virtual ICollection<InventoryDetail> Products { get; set; }

        public Inventory(string name)
        {
            Name = name;

            Products = new List<InventoryDetail>();
        }
    }
}
