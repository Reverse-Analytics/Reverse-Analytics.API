using ReverseAnalytics.Domain.DTOs.Product;
using ReverseAnalytics.Domain.DTOs.Supply;

namespace ReverseAnalytics.Domain.DTOs.SupplyDetail
{
    public record SupplyDetailDto(int Id, int Quantity, decimal UnitPrice,
        SupplyDto Supply, ProductDto Product);
}
