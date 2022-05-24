using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class ContactDetails : BaseAuditableEntity
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public ICollection<Email> Emails { get; set; }
        public ICollection<Phone> Phones { get; set; }

        public ContactDetails()
        {
            Emails = new List<Email>();
            Phones = new List<Phone>();
        }
    }
}
