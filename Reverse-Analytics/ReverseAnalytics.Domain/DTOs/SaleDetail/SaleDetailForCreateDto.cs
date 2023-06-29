namespace ReverseAnalytics.Domain.DTOs.SaleDetail
{
    public record SaleDetailForCreateDto(int Quantity, decimal UnitPrice, double Discount, int SaleId, int ProductId);
}
