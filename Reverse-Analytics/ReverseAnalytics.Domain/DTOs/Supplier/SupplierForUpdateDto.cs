namespace ReverseAnalytics.Domain.DTOs.Supplier
{
    public record SupplierForUpdateDto(int Id, string FullName, string? Company,
        string? PhoneNumber, decimal Balance, bool IsActive);
}
