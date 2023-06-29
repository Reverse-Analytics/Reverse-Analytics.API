using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.DTOs.SupplyDebt
{
    public record SupplyDebtForCreateDto(decimal TotalDue, DateTime DueDate, DateTime? ClosedDate,
        DebtStatus Status, int SupplyId);
}
