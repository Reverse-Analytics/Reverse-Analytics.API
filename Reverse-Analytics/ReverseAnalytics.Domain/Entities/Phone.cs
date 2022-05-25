using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class Phone : BaseAuditableEntity
    {
        public bool? IsPrimary { get; set; }
        public string PhoneNumber { get; set; }

        public int ContactDetailId { get; set; }
        public virtual ContactDetail ContactDetail { get; set; }

        public Phone(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
    }
}
