using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class ProductCategory : BaseAuditableEntity
    {
        public string CategoryName { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public ProductCategory()
        {
            CategoryName = String.Empty;
            Products = new List<Product>();
        }

        public ProductCategory(string categoryName)
        {
            CategoryName = categoryName;
            Products = new List<Product>();
        }
    }
}
