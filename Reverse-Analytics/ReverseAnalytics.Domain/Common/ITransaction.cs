using ReverseAnalytics.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReverseAnalytics.Domain.Common
{
    internal interface ITransaction
    {
        [NotMapped]
        TransactionType TransactionType { get; }
        [NotMapped]
        TransactionSource TransactionSource { get; }
    }
}
