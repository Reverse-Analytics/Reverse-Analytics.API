namespace ReverseAnalytics.Domain.ResourceParameters
{
    public class ProductResourceParamters : ResourceParametersBase
    {
        public decimal? PriceLessThan { get; init; }
        public decimal? PriceGreaterThan { get; init; }
        public decimal? PriceEqualTo { get; set; }
        public int? CategoryId { get; set; }
    }
}
