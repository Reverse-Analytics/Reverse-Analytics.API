using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class Email : BaseAuditableEntity
    {
        public bool IsPrimary { get; set; }
        public string EmailAddress { get; set; }

        public int ContactDetailsId { get; set; }
        public ContactDetails ContactDetails { get; set; }

        public Email(string emailAddress)
        {
            EmailAddress = emailAddress;
        }
    }
}
