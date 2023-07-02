using ReverseAnalytics.Domain.DTOs.Product;

namespace ReverseAnalytics.Domain.DTOs.ProductCategory
{

    public class ProductCategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<ProductDto> Products { get; set; }

        public ProductCategoryDto()
        {
            Products = new HashSet<ProductDto>();
        }
    }
}
