using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class Customer : Person
    {
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<CustomerDebt> CustomerDebts { get; set; }
        public virtual ICollection<CustomerPhone> CustomerPhones { get; set; }
        public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; }

        public Customer(string firstName, string lastName, string? companyName)
            : base(firstName, lastName, companyName)
        {
            Orders = new List<Order>();
        }
    }
}
