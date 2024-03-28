using ReverseAnalytics.Domain.Enums;
using ReverseAnalytics.Domain.ResourceParameters;

namespace ReverseAnalytics.Domain.QueryParameters;

public class TransactionQueryParameters : PaginatedQueryParameters
{
    public DateTime? Date { get; set; }
    public decimal? Amount { get; set; }
    public TransactionType? Type { get; set; }
    public TransactionSource? Source { get; set; }
}
