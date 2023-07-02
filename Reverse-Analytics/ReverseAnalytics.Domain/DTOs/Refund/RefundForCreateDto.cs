using ReverseAnalytics.Domain.DTOs.RefundDetail;

namespace ReverseAnalytics.Domain.DTOs.Refund
{
    public class RefundForCreateDto
    {
        public DateTime RefundDate { get; set; }
        public string? Reason { get; set; }
        public string? ReceivedBy { get; set; }

        public int SaleId { get; set; }

        public ICollection<RefundDetailDto> RefundDetails { get; set; }
    }
}
