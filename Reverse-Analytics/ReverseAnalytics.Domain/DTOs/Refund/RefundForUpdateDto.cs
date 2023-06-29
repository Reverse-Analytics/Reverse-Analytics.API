namespace ReverseAnalytics.Domain.DTOs.Refund
{
    public record RefundForUpdateDto(int Id, string? Reason, string? ReceivedBy,
        DateTime RefundDate, int SaleId);
}
