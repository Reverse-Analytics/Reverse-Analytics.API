using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.Common;

public interface ITransaction
{
    int TransactionSourceId();
    decimal Amount();
    TransactionType TransactionType { get; }
    TransactionSource TransactionSource { get; }
    PaymentType PaymentType { get; }
    CurrencyType Currency { get; }
}
