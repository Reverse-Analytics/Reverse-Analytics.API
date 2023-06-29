using ReverseAnalytics.Domain.DTOs.Product;
using ReverseAnalytics.Domain.DTOs.Refund;

namespace ReverseAnalytics.Domain.DTOs.RefundDetail
{
    public record RefundDetailDto(int Id, int Quantity, RefundDto Refund, ProductDto Product);
}
