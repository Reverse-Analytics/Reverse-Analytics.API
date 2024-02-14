using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class ProductCategory : BaseAuditableEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public ProductCategory()
        {
            Name = String.Empty;
            Products = new List<Product>();
        }

        public ProductCategory(string name)
        {
            Name = name;
            Products = new List<Product>();
        }
    }
}
