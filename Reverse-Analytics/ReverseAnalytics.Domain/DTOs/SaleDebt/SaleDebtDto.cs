using ReverseAnalytics.Domain.DTOs.Sale;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.DTOs.SaleDebt
{
    public record SaleDebtDto(int Id, decimal TotalDue, DateTime DueDate, DateTime? ClosedDate,
        DebtStatus Status, SaleDto Sale);
}
