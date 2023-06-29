namespace ReverseAnalytics.Domain.DTOs.Supply
{
    public record SupplyForCreateDto(string? ReceivedBy, string? Comment, DateTime? SupplyDate,
        decimal TotalDue, decimal TotalPaid, int SupplierId);
}
