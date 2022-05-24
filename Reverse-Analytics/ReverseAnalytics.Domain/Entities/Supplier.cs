using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class Supplier : BaseAuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? CompanyName { get; set; }
        public string? Address { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }

        public Supplier(string firstName, string lastName, string? companyName, string? address)
        {
            FirstName = firstName;
            LastName = lastName;
            CompanyName = companyName;
            Address = address;

            Purchases = new List<Purchase>();
        }
    }
}
