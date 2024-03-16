using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.Entities;

public class Supply : BaseAuditableEntity, ITransaction, IRefundable, IDebtable
{
    public DateTime Date { get; set; }
    public string? Comments { get; set; }
    public decimal TotalDue { get; set; }
    public decimal TotalPaid { get; set; }

    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; }

    public virtual ICollection<SupplyItem> SupplyItems { get; set; }

    public TransactionType TransactionType => TransactionType.Expense;
    public TransactionSource TransactionSource => TransactionSource.Supply;
    public int RefundSourceId => Id;
    public RefundSource RefundSource => RefundSource.Supply;
    public DebtSource DebtSource => DebtSource.Supply;

    public Supply()
    {
        SupplyItems = [];
    }
}
