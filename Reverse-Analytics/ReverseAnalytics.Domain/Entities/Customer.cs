using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class Customer : Person
    {
        public virtual ICollection<Order> Orders { get; set; }

        public Customer(string firstName, string lastName, string address, string? companyName)
            : base(firstName, lastName, address, companyName)
        {
            Orders = new List<Order>();
        }
    }
}
