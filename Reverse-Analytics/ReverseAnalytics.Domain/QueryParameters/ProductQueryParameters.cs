namespace ReverseAnalytics.Domain.QueryParameters;

public class ProductQueryParameters : BaseQueryParameters
{
    public decimal? Price { get; set; }
    public int? CategoryId { get; set; }
}
