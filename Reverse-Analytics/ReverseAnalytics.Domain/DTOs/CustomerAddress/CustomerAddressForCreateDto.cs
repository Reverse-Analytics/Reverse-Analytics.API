namespace ReverseAnalytics.Domain.DTOs.CustomerAddress
{
    public class CustomerAddressForCreateDto
    {
        public string AddressDetails { get; set; }
        public int CityId { get; set; }
        public int CustomerId { get; set; }
    }
}
