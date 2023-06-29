using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.DTOs.Sale
{
    public record SaleForUpdateDto(int Id, string Receipt, string? Comments,
        string? SoldBy, decimal TotalDue, decimal TotalPaid, decimal TotalDiscount,
        SaleType SaleType, DateTime SaleDate, int CustomerId);
}
