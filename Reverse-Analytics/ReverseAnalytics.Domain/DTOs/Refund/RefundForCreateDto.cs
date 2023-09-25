using ReverseAnalytics.Doman.DTOs.RefundItem;

namespace ReverseAnalytics.Domain.DTOs.Refund
{
    public class RefundForCreateDto
    {
        public DateTime RefundDate { get; set; }
        public string? Reason { get; set; }
        public string? ReceivedBy { get; set; }

        public int SaleId { get; set; }

        public ICollection<RefundItemDto> RefundDetails { get; set; }
    }
}
