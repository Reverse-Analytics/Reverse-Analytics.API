using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.DTOs.SaleDebt
{
    public record SaleDebtForUpdateDto(int Id, decimal TotalDue, DateTime DueDate, DateTime? ClosedDate,
        DebtStatus Status, int SaleId);
}
