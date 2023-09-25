namespace ReverseAnalytics.Domain.DTOs.SaleItem
{
    public class SaleItemForUpdateDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public double DiscountPercentage { get; set; }

        public int SaleId { get; set; }
        public int ProductId { get; set; }
    }
}
