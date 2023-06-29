namespace ReverseAnalytics.Domain.DTOs.Customer
{
    public record CustomerForUpdateDto(int Id, string FullName, string? CompanyName, string? Address,
        string? PhoneNumber, decimal Balance, double Discount, bool IsActive);
}
