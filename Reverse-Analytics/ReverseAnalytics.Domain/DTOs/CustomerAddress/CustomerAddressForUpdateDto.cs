namespace ReverseAnalytics.Domain.DTOs.CustomerAddress
{
    public class CustomerAddressForUpdateDto
    {
        public int CustomerAddressId { get; set; }
        public string CustomerAddressDetails { get; set; }
        public int? CityId { get; set; }
        public int CustomerId { get; set; }
    }
}
