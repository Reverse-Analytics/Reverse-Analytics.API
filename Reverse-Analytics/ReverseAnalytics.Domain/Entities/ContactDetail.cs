using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class ContactDetail : BaseAuditableEntity
    {
        public int PersonId { get; set; }
        public Person Person { get; set; }

        public virtual ICollection<Email> Emails { get; set; }
        public virtual ICollection<Phone> Phones { get; set; }

        public ContactDetail()
        {
            Emails = new List<Email>();
            Phones = new List<Phone>();
        }
    }
}
