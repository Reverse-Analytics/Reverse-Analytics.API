using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public abstract class Person : BaseAuditableEntity
    {
        public string FullName { get; set; }
        public string? CompanyName { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Phone> Phones { get; set; }
        public virtual ICollection<Debt> Debts { get; set; }

        public Person()
        {
        }

        public Person(string fullName)
        {
            FullName = fullName;

            Addresses = new List<Address>();
            Phones = new List<Phone>();
            Debts = new List<Debt>();
        }
    }
}
