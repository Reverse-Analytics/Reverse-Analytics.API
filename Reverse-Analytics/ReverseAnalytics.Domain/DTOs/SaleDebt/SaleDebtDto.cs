using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.DTOs.SaleDebt
{
    public class SaleDebtDto
    {
        public int Id { get; set; }
        public string Receipt { get; set; }
        public string Customer { get; set; }
        public string SoldBy { get; set; }
        public string ClosedBy { get; set; }
        public decimal TotalDue { get; set; }
        public decimal TotalPaid { get; set; }
        public DateTime DebtDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public DebtStatus Status { get; set; }
    }
}
