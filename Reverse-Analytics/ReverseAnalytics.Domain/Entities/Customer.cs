using ReverseAnalytics.Domain.Common;
using System.Collections.Generic;

namespace ReverseAnalytics.Domain.Entities
{
    public class Customer : BaseAuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public ICollection<Sale> Sales { get; set; }

        public Customer()
        {
            Sales = new List<Sale>();
        }
    }
}
