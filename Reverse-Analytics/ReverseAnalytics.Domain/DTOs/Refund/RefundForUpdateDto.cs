using ReverseAnalytics.Doman.DTOs.RefundItem;

namespace ReverseAnalytics.Domain.DTOs.Refund
{
    public class RefundForUpdateDto
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DebtPaymentAmount { get; set; }
        public decimal RefundAmount { get; set; }
        public DateTime RefundDate { get; set; }
        public string? Reason { get; set; }
        public string? ReceivedBy { get; set; }

        public int SaleId { get; set; }

        public ICollection<RefundItemForUpdateDto> RefundItems { get; set; }
    }
}
