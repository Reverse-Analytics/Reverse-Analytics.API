using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.Common;

public interface IRefundable
{
    int RefundSourceId { get; }
    RefundSource RefundSource { get; }
}
