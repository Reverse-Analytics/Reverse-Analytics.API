using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.Entities;

public class Refund : BaseAuditableEntity, ITransaction, IDebtable
{
    public DateTime Date { get; set; }
    public string ReceivedBy { get; set; }
    public string? Reason { get; set; }
    public string? Comments { get; set; }
    public int SourceId { get; set; }
    public decimal TotalDue { get; set; }
    public decimal TotalPaid { get; set; }
    public RefundSource Source { get; set; }

    public TransactionType TransactionType
        => Source == RefundSource.Sale ? TransactionType.Expense : TransactionType.Income;
    public TransactionSource TransactionSource => TransactionSource.Refund;
    public DebtSource DebtSource
        => Source == RefundSource.Sale ? DebtSource.SaleRefund : DebtSource.SupplyRefund;
}