using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class Supplier : BaseAuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }

        public Supplier(string firstName, string lastName, string companyName)
        {
            FirstName = firstName;
            LastName = lastName;
            CompanyName = companyName;
        }
    }
}
