using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class Customer : BaseAuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Address { get; set; }
        public string? CompanyName { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public Customer(string firstName, string lastName, string address, string? companyName)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            CompanyName = companyName;

            Orders = new List<Order>();
        }
    }
}
