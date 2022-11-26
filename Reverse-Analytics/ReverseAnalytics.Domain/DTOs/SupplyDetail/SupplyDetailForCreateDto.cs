namespace ReverseAnalytics.Domain.DTOs.SupplyDetail
{
    public class SupplyDetailForCreateDto
    {
        public int Quntity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? UnitPriceDiscount { get; set; }

        public int SupplyId { get; set; }
        public int ProductId { get; set; }
    }
}