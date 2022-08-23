using ReverseAnalytics.Domain.DTOs.City;
using ReverseAnalytics.Domain.DTOs.Customer;

namespace ReverseAnalytics.Domain.DTOs.CustomerAddress
{
    public class CustomerAddressForCreateDto
    {
        public string AddressDetails { get; set; }
        public CityDto City { get; set; }
        public CustomerDto Customer { get; set; }
    }
}
