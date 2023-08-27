using ReverseAnalytics.Domain.DTOs.Supply;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.DTOs.SupplyDebt
{
    public class SupplyDebtDto
    {
        public int Id { get; set; }
        public decimal TotalDue { get; set; }
        public decimal Leftover { get; set; } = 0;
        public DateTime DueDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public DebtStatus Status { get; set; }

        public SupplyDto Supply { get; set; }
    }
}
