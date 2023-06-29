using ReverseAnalytics.Domain.DTOs.Supplier;

namespace ReverseAnalytics.Domain.DTOs.Supply
{
    public record SupplyDto(int Id, string? ReceivedBy, string? Comment,
        DateTime? SupplyDate, decimal TotalDue, decimal TotalPaid,
        SupplierDto Supplier);
}
