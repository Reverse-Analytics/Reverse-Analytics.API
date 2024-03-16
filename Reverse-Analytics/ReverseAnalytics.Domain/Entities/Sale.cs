﻿using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.Entities;

public class Sale : BaseAuditableEntity, ITransaction, IRefundable, IDebtable
{
    public DateTime Date { get; set; }
    public string? Comments { get; set; }
    public decimal TotalDue { get; set; }
    public decimal TotalPaid { get; set; }
    public decimal TotalDiscount { get; set; }
    public SaleType SaleType { get; set; }
    public SaleStatus Status { get; set; }
    public PaymentType PaymentType { get; set; }
    public CurrencyType Currency { get; set; }

    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; }

    public virtual ICollection<SaleItem> SaleItems { get; set; }

    public TransactionType TransactionType => TransactionType.Income;
    public TransactionSource TransactionSource => TransactionSource.Sale;
    public int RefundSourceId => Id;
    public RefundSource RefundSource => RefundSource.Sale;
    public DebtSource DebtSource => DebtSource.Sale;

    public Sale()
    {
        SaleItems = [];
    }
}
