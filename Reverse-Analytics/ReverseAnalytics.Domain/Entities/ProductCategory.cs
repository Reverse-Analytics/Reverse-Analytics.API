using ReverseAnalytics.Domain.Common;
using System.Collections.Generic;

namespace ReverseAnalytics.Domain.Entities
{
    public class ProductCategory : BaseAuditableEntity
    {
        public string CategoryName { get; set; }

        public ICollection<Product> Products { get; set; }

        public ProductCategory()
        {
            Products = new List<Product>();
        }
    }
}
