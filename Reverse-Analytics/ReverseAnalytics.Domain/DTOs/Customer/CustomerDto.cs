using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.DTOs.Customer
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? CompanyName { get; set; }

        public ICollection<Entities.CustomerAddress> CustomerAddresses { get; set; }
        public ICollection<CustomerPhone> CustomerPhones { get; set; }
        public ICollection<CustomerDebt> CustomerDebts { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
