using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public abstract class Person : BaseAuditableEntity
    {
        public string FullName { get; set; }
        public string? CompanyName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }

        public Person()
        {
        }

        public Person(string fullName)
        {
            FullName = fullName;
        }
    }
}
