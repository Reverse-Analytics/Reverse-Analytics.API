namespace ReverseAnalytics.Domain.Entities
{
    public class Supplier : Person
    {
        public virtual ICollection<Supply> Supplies { get; set; }

        public Supplier()
        {
        }

        public Supplier(string fullName, string? companyName)
        {
            FullName = fullName;
            CompanyName = companyName;

            Supplies = new List<Supply>();
        }
    }
}
