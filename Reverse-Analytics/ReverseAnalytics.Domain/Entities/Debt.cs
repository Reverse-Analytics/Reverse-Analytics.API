using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.Entities;

public class Debt : BaseAuditableEntity
{
    public DateTime Date { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? ClosedDate { get; set; }
    public int SourceId { get; set; }
    public int DebtorId { get; set; }
    public decimal TotalDue { get; set; }
    public decimal TotalPaid { get; set; }
    public decimal Leftover { get; set; }
    public DebtSource Source { get; set; }
    public DebtorType DebtorType { get; set; }

    public virtual ICollection<DebtPayment> Payments { get; set; }

    public Debt()
    {
        Payments = [];
    }
}
