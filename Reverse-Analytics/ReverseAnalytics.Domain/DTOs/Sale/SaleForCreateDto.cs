using ReverseAnalytics.Domain.DTOs.SaleDetail;
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
        public double? TotalDiscount { get; set; }
        public SaleType SaleType { get; set; }
        public DateTime SaleDate { get; set; }

        public int CustomerId { get; set; }

        public ICollection<SaleDetailForCreateDto> SaleDetails { get; set; }

        public SaleForCreateDto()
        {
            SaleDetails = new List<SaleDetailForCreateDto>();
        }
    }
}
