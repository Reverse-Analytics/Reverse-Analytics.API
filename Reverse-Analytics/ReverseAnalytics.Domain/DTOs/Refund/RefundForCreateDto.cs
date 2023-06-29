namespace ReverseAnalytics.Domain.DTOs.Refund
{
    public record RefundForCreateDto(string? Reason, string? ReceivedBy, DateTime RefundDate, int SaleId);
}
