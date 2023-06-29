using ReverseAnalytics.Domain.DTOs.Sale;

namespace ReverseAnalytics.Domain.DTOs.Customer
{
    public record CustomerDto(int Id, string FullName, string? Address, string? PhoneNumber,
        string? Company, decimal Balance, double Discount,
        bool IsActive, ICollection<SaleDto> Sales);
}
