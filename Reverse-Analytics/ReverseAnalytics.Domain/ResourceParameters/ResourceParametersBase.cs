namespace ReverseAnalytics.Domain.ResourceParameters
{
    public abstract class ResourceParametersBase
    {
        public string? SearchQuery { get; init; }
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
    }
}
