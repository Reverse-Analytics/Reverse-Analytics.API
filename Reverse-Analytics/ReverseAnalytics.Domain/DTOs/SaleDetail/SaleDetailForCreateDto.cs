namespace ReverseAnalytics.Domain.DTOs.SaleDetail
{
    public class SaleDetailForCreateDto
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? UnitPriceDiscount { get; set; }

        public int SaleId { get; set; }
        public int ProductId { get; set; }
    }
}
