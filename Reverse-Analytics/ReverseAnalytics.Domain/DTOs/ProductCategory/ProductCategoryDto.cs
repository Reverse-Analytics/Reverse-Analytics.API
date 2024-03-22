using ReverseAnalytics.Domain.DTOs.Product;

namespace ReverseAnalytics.Domain.DTOs.ProductCategory;

public class ProductCategoryDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string? Description { get; init; }

    public int? ParentId { get; init; }
    public ProductCategoryDto? Parent { get; init; }

    public ICollection<ProductDto> Products { get; init; }
    public ICollection<ProductCategoryDto> SubCategories { get; init; }

    public ProductCategoryDto()
    {
        Name = string.Empty;
        Products = [];
        SubCategories = [];
    }
}
