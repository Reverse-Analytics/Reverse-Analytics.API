using ReverseAnalytics.Domain.DTOs.RefundDetail;
using ReverseAnalytics.Domain.DTOs.Sale;

namespace ReverseAnalytics.Domain.DTOs.Refund
{
    public record RefundDto(int Id, string? Reason, string? ReceivedBy,
        DateTime RefundDate, SaleDto Sale, ICollection<RefundDetailDto> RefundDetails);
}
