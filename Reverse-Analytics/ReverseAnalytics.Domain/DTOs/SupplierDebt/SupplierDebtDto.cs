namespace ReverseAnalytics.Domain.DTOs.SupplierDebt
{
    public class SupplierDebtDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public int SupplierId { get; set; }
    }
}
