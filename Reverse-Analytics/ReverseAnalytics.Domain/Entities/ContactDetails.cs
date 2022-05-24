using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class ContactDetails : BaseAuditableEntity
    {
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }

        public virtual ICollection<Email> Emails { get; set; }
        public virtual ICollection<Phone> Phones { get; set; }

        public ContactDetails()
        {
            Emails = new List<Email>();
            Phones = new List<Phone>();
        }
    }
}
