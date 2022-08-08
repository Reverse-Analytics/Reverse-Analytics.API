using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class City : BaseAuditableEntity
    {
        public string CityName { get; set; }

        public ICollection<Address> Addresses { get; set; }
    }
}
