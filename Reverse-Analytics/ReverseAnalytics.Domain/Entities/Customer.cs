using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class Customer : BaseAuditableEntity
    {
        public string FullName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Company { get; set; }
        public decimal Balance { get; set; }
        public double Discount { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }

        public Customer()
        {
            Sales = new List<Sale>();
        }
    }
}
