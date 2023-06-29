using ReverseAnalytics.Domain.DTOs.Product;

namespace ReverseAnalytics.Domain.DTOs.ProductCategory
{
    public record ProductCategoryDto(int Id, string CategoryName, ICollection<ProductDto> Products);
}
