﻿using ReverseAnalytics.Domain.ResourceParameters;

namespace ReverseAnalytics.Domain.QueryParameters;

public class SupplierQueryParameters : PaginatedQueryParameters
{
    public decimal? Balance { get; set; }
}
