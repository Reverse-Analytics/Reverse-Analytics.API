﻿using ReverseAnalytics.Domain.Enums;
using ReverseAnalytics.Domain.ResourceParameters;

namespace ReverseAnalytics.Domain.QueryParameters;

public class SaleQueryParameters : PaginatedQueryParameters
{
    public DateTime? SaleDate { get; set; }
    public decimal? TotalDue { get; set; }
    public SaleStatus? Status { get; set; }
    public int? CustomerId { get; set; }
}
