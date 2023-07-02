using ReverseAnalytics.Domain.DTOs.RefundDetail;
using ReverseAnalytics.Domain.DTOs.Sale;

namespace ReverseAnalytics.Domain.DTOs.Refund
{
    public class RefundDto
    {
        public int Id { get; set; }
        public DateTime RefundDate { get; set; }
        public string? Reason { get; set; }
        public string? ReceivedBy { get; set; }

        public int SaleId { get; set; }
        public virtual SaleDto Sale { get; set; }

        public ICollection<RefundDetailDto> RefundDetails { get; set; }
    }
}
