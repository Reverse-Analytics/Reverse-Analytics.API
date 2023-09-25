using ReverseAnalytics.Domain.DTOs.SaleItem;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.DTOs.Sale
{
    public class SaleForCreateDto
    {
        public string Receipt { get; set; }
        public string? Comments { get; set; }
        public string? SoldBy { get; set; }
        public decimal TotalDue { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal TotalDiscount { get; set; }
        public double TotalDiscountPercentage { get; set; }
        public SaleType SaleType { get; set; }
        public DateTime SaleDate { get; set; }

        public int CustomerId { get; set; }

        public ICollection<SaleItemForCreateDto> SaleDetails { get; set; }

        public SaleForCreateDto()
        {
            SaleDetails = new List<SaleItemForCreateDto>();
        }
    }
}
