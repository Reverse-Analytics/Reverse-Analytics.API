using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class Supplier : BaseAuditableEntity
    {
        public string FullName { get; set; }
        public string? CompanyName { get; set; }
        public string? PhoneNumber { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Supply> Supplies { get; set; }
        public virtual ICollection<SupplierDebt> SupplierDebts { get; set; }
        public virtual ICollection<SupplierPhone> SupplierPhones { get; set; }
        public virtual ICollection<SupplierAddress> Addresses { get; set; }

        public Supplier()
        {
        }

        public Supplier(string fullName, string? companyName, string? phoneNumber)
        {
            FullName = fullName;
            CompanyName = companyName;
            PhoneNumber = phoneNumber;

            Supplies = new List<Supply>();
            SupplierDebts = new List<SupplierDebt>();
            SupplierPhones = new List<SupplierPhone>();
        }
    }
}
