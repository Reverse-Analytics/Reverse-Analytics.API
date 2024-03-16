using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.Common
{
    public interface IDebtable
    {
        DebtSource DebtSource { get; }
    }
}
