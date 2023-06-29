using ReverseAnalytics.Domain.DTOs.Product;
using ReverseAnalytics.Domain.DTOs.Sale;

namespace ReverseAnalytics.Domain.DTOs.SaleDetail
{
    public record SaleDetailDto(int Id, int Quantity, decimal UnitPrice, double Discount,
        SaleDto Sale, ProductDto Product);
}
