using ReverseAnalytics.Domain.DTOs.Supply;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.DTOs.SupplyDebt
{
    public record SupplyDebtDto(int Id, decimal TotalDue, DateTime DueDate,
        DateTime? ClosedDate, DebtStatus Status, SupplyDto Supply);
}
