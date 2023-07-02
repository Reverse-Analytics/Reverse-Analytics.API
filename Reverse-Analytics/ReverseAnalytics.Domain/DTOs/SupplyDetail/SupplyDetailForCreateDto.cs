namespace ReverseAnalytics.Domain.DTOs.SupplyDetail
{
    public class SupplyDetailForCreateDto
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public int SupplyId { get; set; }
        public int ProductId { get; set; }
    }
}