using ReverseAnalytics.Domain.DTOs.Customer;
using ReverseAnalytics.Domain.DTOs.Refund;
using ReverseAnalytics.Domain.DTOs.SaleDebt;
using ReverseAnalytics.Domain.DTOs.SaleDetail;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.DTOs.Sale
{
    public record SaleDto(int Id, string Receipt, string? Comments,
        string? SoldBy, decimal TotalDue, decimal TotalPaid, decimal TotalDiscount,
        SaleType SaleType, DateTime SaleDate, CustomerDto Customer,
        ICollection<SaleDetailDto> OrderDetails, ICollection<SaleDebtDto> SaleDebts, ICollection<RefundDto> Refunds);
}
