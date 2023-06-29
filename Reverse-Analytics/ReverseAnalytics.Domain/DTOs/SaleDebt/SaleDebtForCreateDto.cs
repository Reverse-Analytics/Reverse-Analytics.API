using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.DTOs.SaleDebt
{
    public record SaleDebtForCreateDto(decimal TotalDue, DateTime DueDate, DateTime? ClosedDate,
        DebtStatus Status, int SaleId);
}
