namespace ReverseAnalytics.Domain.Entities
{
    public class Supplier : Person
    {
        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<SupplierDebt> SupplierDebts { get; set; }
        public virtual ICollection<SupplierPhone> SupplierPhones { get; set; }

        public Supplier(string firstName, string lastName, string? companyName)
            : base(firstName, lastName, companyName)
        {
            Purchases = new List<Purchase>();
        }
    }
}
