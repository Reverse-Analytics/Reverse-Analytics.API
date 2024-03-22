using ReverseAnalytics.Domain.DTOs.ProductCategory;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.DTOs.Product;

public class ProductDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Code { get; init; }
    public string? Description { get; init; }
    public decimal SalePrice { get; init; }
    public decimal SupplyPrice { get; init; }
    public double? Volume { get; init; }
    public double? Weight { get; init; }
    public UnitOfMeasurement UnitOfMeasurement { get; init; }

    public int CategoryId { get; init; }
    public ProductCategoryDto Category { get; init; }
}
