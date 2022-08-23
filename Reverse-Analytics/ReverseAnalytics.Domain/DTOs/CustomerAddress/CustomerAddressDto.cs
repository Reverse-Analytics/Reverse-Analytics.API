using ReverseAnalytics.Domain.DTOs.City;
using ReverseAnalytics.Domain.DTOs.Customer;

namespace ReverseAnalytics.Domain.DTOs.CustomerAddress
{
    public class CustomerAddressDto
    {
        public int CustomerAddressId { get; set; }
        public string AddressDetails { get; set; }

        public int? CityId { get; set; }
        public CityDto? City { get; set; }
        public int CustomerId { get; set; }
        public CustomerDto Customer { get; set; }
    }
}
