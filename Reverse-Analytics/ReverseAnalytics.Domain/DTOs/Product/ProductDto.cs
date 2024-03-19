using ReverseAnalytics.Domain.DTOs.ProductCategory;
using ReverseAnalytics.Domain.DTOs.SaleItem;
using ReverseAnalytics.Domain.DTOs.SupplyItem;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.DTOs.Product;

public record ProductDto(
    int Id,
    string ProductName,
    string ProductCode,
    string? Description,
    decimal SalePrice,
    decimal SupplyPrice,
    double? Volume,
    double? Weight,
    UnitOfMeasurement UnitOfMeasurement,
    ProductCategoryDto Category,
    ICollection<SaleItemDto> SaleItems,
    ICollection<SupplyItemDto> SupplyItems);
