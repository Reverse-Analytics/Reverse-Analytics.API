﻿namespace ReverseAnalytics.Domain.Entities
{
    public class SupplierDebt
    {
        public int SupplierDebtId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DebtDate { get; set; }
        public DateTime DueDate { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}
