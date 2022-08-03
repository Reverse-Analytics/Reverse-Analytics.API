using ReverseAnalytics.Domain.DTOs.City;

namespace ReverseAnalytics.Domain.DTOs.Address
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string AddressDetails { get; set; }

        public int CityId { get; set; }
        public CityDto City { get; set; }
    }
}
