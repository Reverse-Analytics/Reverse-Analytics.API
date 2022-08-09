using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class Customer : BaseAuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? CompanyName { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<CustomerDebt> CustomerDebts { get; set; }
        public virtual ICollection<CustomerPhone> CustomerPhones { get; set; }
        public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; }

        public Customer()
        {
        }

        public Customer(string firstName, string lastName, string? companyName)
        {
            Orders = new List<Order>();
            CustomerDebts = new List<CustomerDebt>();
            CustomerPhones = new List<CustomerPhone>();
            CustomerAddresses = new List<CustomerAddress>();

            FirstName = firstName;
            LastName = lastName;
            CompanyName = companyName;
        }
    }
}
