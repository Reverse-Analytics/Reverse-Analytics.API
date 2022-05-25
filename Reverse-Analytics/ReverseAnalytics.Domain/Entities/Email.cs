using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class Email : BaseAuditableEntity
    {
        public bool? IsPrimary { get; set; }
        public string EmailAddress { get; set; }

        public int ContactDetailId { get; set; }
        public virtual ContactDetail ContactDetail { get; set; }

        public Email(string emailAddress)
        {
            EmailAddress = emailAddress;
        }
    }
}
