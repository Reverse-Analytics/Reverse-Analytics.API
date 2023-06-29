using ReverseAnalytics.Domain.DTOs.Supply;

namespace ReverseAnalytics.Domain.DTOs.Supplier
{
    public record SupplierDto(int Id, string FullName, string? Company,
        string? PhoneNumber, decimal Balance, bool IsActive,
        ICollection<SupplyDto> Supplies);
}
