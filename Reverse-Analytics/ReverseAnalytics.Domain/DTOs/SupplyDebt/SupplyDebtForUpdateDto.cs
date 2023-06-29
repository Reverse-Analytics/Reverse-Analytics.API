using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.DTOs.SupplyDebt
{
    public record SupplyDebtForUpdateDto(int Id, decimal TotalDue, DateTime DueDate, DateTime? ClosedDate, DebtStatus Status, int SupplyId);
}
