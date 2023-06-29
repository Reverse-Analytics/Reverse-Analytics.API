namespace ReverseAnalytics.Domain.DTOs.Supplier
{
    public record SupplierForCreateDto(string FullName, string? Company,
        string? PhoneNumber, decimal Balance, bool IsActive);
}
