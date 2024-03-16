using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.Entities;

public class DebtPayment : BaseAuditableEntity, IDebtable
{
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }

    public int DebtId { get; set; }
    public Debt Debt { get; set; }

    public DebtSource DebtSource => DebtSource.Debt;
}
