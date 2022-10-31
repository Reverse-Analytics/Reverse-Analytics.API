using ReverseAnalytics.Domain.DTOs.Supplier;

namespace ReverseAnalytics.Domain.DTOs.Supply
{
    public class SupplyDto
    {
        public int Id { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public decimal TotalDue { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal? DebtAmount { get; set; }
        public string? ReceivedBy { get; set; }

        public int SupplierId { get; set; }
        public SupplierDto Supplier { get; set; }
    }
}
