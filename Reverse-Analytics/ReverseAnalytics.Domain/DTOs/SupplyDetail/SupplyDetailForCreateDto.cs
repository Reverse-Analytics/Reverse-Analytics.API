namespace ReverseAnalytics.Domain.DTOs.SupplyDetail
{
    public record SupplyDetailForCreateDto(int Quantity, decimal UnitPrice, int SupplyId, int ProductId);
}