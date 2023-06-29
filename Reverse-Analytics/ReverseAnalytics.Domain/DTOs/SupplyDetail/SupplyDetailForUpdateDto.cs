namespace ReverseAnalytics.Domain.DTOs.SupplyDetail
{
    public record SupplyDetailForUpdateDto(int Id, int Quantity, decimal UnitPrice, int SupplyId, int ProductId);
}
