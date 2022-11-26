﻿using ReverseAnalytics.Domain.DTOs.CustomerPhoneDto;

namespace ReverseAnalytics.Domain.DTOs.CustomerPhone
{
    public class CustomerPhoneDto
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsPrimary { get; set; }

        public int CustomerId { get; set; }
    }
}