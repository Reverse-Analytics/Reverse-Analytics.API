using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class Supplier : BaseAuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? CompanyName { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<SupplierDebt> SupplierDebts { get; set; }
        public virtual ICollection<SupplierPhone> SupplierPhones { get; set; }

        public Supplier(string firstName, string lastName, string? companyName)
        {
            FirstName = firstName;
            LastName = lastName;
            CompanyName = companyName;

            Purchases = new List<Purchase>();
            SupplierDebts = new List<SupplierDebt>();
            SupplierPhones = new List<SupplierPhone>();
        }
    }
}
