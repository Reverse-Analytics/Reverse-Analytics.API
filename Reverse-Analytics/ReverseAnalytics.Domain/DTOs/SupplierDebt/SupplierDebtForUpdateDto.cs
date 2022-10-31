namespace ReverseAnalytics.Domain.DTOs.SupplierDebt
{
    public class SupplierDebtForUpdateDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public int SupplierId { get; set; }
    }
}
