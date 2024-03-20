namespace ReverseAnalytics.Domain.Common;

public class PaginatedList<T> : List<T>
{
    public int CurrentPage { get; private set; }
    public int PageSize { get; private set; }
    public int PagesCount { get; private set; }
    public int TotalCount { get; private set; }
    public bool HasNext => CurrentPage < PagesCount;
    public bool HasPrevious => CurrentPage > 1;

    public PaginatedList(List<T> items, int currentPage, int pageSize, int totalCount)
    {
        TotalCount = totalCount;
        CurrentPage = currentPage;
        PageSize = pageSize;
        PagesCount = (int)Math.Ceiling(totalCount / (double)pageSize);
        AddRange(items);
    }
}