using ReverseAnalytics.Domain.Common;
using System;

namespace ReverseAnalytics.Domain.Entities
{
    public class Sale : BaseAuditableEntity
    {
        public DateTime SaleDate { get; set; }

    }
}
