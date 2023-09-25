namespace ReverseAnalytics.Domain.DTOs.Refund
{
    public class RefundForUpdateDto
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime RefundDate { get; set; }
        public string? Reason { get; set; }
        public string? ReceivedBy { get; set; }

        public int SaleId { get; set; }
    }
}
