using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class Address : BaseEntity
    {
        public string AddressDetails { get; set; }
        public string? AddressLandMark { get; set; }
        public string LocationOnMap { get; set; }
        public string Longtitude { get; set; }
        public string Latitude { get; set; }

        public int CustomerAddressId { get; set; }
        public CustomerAddress CustomerAddress { get; set; }
        public int SupplierAddressId { get; set; }
        public SupplierAddress SupplierAddress { get; set; }
    }
}
