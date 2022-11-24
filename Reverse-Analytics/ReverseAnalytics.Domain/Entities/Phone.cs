using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class Phone : BaseAuditableEntity
    {
        public string PhoneNumber { get; set; }
        public bool IsPrimary { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
