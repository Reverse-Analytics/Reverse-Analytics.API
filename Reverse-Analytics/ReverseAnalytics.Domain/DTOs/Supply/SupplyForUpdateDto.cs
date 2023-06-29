namespace ReverseAnalytics.Domain.DTOs.Supply
{
    public record SupplyForUpdateDto(int Id, string? ReceivedBy, string? Comment,
        DateTime? SupplyDate, decimal TotalDue, decimal TotalPaid,
        int SupplierId);
}
