using ReverseAnalytics.Domain.DTOs.SaleDetail;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.DTOs.Sale
{
    public record SaleForCreateDto(string? Comments, string? SoldBy,
        decimal TotalDue, decimal TotalPaid, decimal TotalDiscount,
        SaleType SaleType, DateTime SaleDate, int CustomerId, IEnumerable<SaleDetailForCreateDto>? SaleDetails);
}
