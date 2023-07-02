using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.DTOs.SaleDebt
{
    public class SaleDebtForUpdateDto
    {
        public int Id { get; set; }
        public decimal TotalDue { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public DebtStatus Status { get; set; }
        public int SaleId { get; set; }
    }
}
