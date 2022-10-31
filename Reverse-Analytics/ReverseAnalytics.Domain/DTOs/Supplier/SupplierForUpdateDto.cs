﻿namespace ReverseAnalytics.Domain.DTOs.Supplier
{
    public class SupplierForUpdateDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? CompanyName { get; set; }

        public int SupplierId { get; set; }
    }
}