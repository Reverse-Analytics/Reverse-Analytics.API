namespace ReverseAnalytics.Domain.DTOs.Supply
{
    public class SupplyForCreate
    {
        public DateTime? PurchaseDate { get; set; }
        public decimal TotalDue { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal? DebtAmount { get; set; }
        public string? ReceivedBy { get; set; }

        public int SupplierId { get; set; }
    }
}
