using ReverseAnalytics.Domain.QueryParameters;

namespace ReverseAnalytics.Domain.ResourceParameters;

public class PaginatedQueryParameters : QueryParametersBase
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}
