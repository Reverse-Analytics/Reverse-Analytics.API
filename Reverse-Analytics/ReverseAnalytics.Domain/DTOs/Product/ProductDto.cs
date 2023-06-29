using ReverseAnalytics.Domain.DTOs.ProductCategory;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.DTOs.Product
{
    public record ProductDto(int Id, string ProductName, string ProductCode, string? Description,
        decimal SalePrice, decimal? SupplyPrice, double QuantityInStock,
        double? Volume, double? Weight, UnitOfMeasurement UnitOfMeasurement,
        ProductCategoryDto Category);
}
