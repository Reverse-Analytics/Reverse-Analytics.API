namespace ReverseAnalytics.Domain.DTOs.SupplyDetail
{
    public class SupplyDetailForUpdateDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? UnitPriceDiscount { get; set; }

        public int SupplyId { get; set; }
        public int ProductId { get; set; }
    }
}
