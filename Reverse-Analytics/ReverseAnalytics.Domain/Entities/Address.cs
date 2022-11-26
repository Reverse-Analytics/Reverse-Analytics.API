using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class Address : BaseAuditableEntity
    {
        public string AddressDetails { get; set; }
        public string? AddressLandMark { get; set; }
        public double Longtitude { get; set; }
        public double Latitude { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
