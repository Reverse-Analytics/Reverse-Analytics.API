﻿using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.Entities
{
    public class SaleDebt : BaseAuditableEntity
    {
        public decimal TotalDue { get; set; }
        public decimal TotalPaid { get; set; }
        public DateTime DebtDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public DebtStatus Status { get; set; }

        public int SaleId { get; set; }
        public virtual Sale Sale { get; set; }
    }
}
