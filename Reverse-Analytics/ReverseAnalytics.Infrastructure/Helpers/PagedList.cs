using Microsoft.EntityFrameworkCore;

namespace ReverseAnalytics.Infrastructure.Helpers
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int PagesCount { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasNext => CurrentPage < PagesCount;
        public bool HasPrevious => CurrentPage > 1;

        private PagedList(List<T> items, int currentPage, int pageSize, int totalCount)
        {
            TotalCount = totalCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
            PagesCount = (int)Math.Ceiling(totalCount / (double)pageSize);
            AddRange(items);
        }

        public static async Task<PagedList<T>> ToPagedListAsync(
            IQueryable<T> source, int pageNumber, int pageSize)
        {
            ArgumentNullException.ThrowIfNull(source);

            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedList<T>(items, pageNumber, pageSize, count);
        }
    }
}
