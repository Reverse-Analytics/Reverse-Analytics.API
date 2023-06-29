namespace ReverseAnalytics.Domain.DTOs.RefundDetail
{
    public record RefundDetailForUpdateDto(int Id, int Quantity, int RefundId, int ProductId);
}
