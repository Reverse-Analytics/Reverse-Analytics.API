namespace ReverseAnalytics.Domain.DTOs.SupplierDebt
{
    public class SupplierDebtForUpdateDto
    {
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public int SupplierId { get; set; }
    }
}
