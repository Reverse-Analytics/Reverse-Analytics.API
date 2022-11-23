using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class Customer : BaseAuditableEntity
    {
        public string FullName { get; set; }
        public string? CompanyName { get; set; }
        public string? PhoneNumber { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<CustomerDebt> CustomerDebts { get; set; }
        public virtual ICollection<CustomerPhone> CustomerPhones { get; set; }
        public virtual ICollection<CustomerAddress> Addresses { get; set; }

        public Customer()
        {
        }

        public Customer(string fullName, string? companyName)
        {
            FullName = fullName;
            CompanyName = companyName;

            Sales = new List<Sale>();
            CustomerDebts = new List<CustomerDebt>();
            CustomerPhones = new List<CustomerPhone>();
        }
    }
}
