namespace ReverseAnalytics.Domain.DTOs.SaleDetail
{
    public record SaleDetailForUpdateDto(int Id, int Quantity, decimal UnitPrice,
        double Discount, int SaleId, int ProductId);
}
