namespace ReverseAnalytics.Domain.Common;

public record PaginationMetaData(
    int TotalCount,
    int PageSize,
    int CurrentPage,
    int TotalPages,
    bool HasNext,
    bool HasPrevious);
