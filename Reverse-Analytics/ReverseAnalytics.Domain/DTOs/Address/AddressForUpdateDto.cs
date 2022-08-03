namespace ReverseAnalytics.Domain.DTOs.Address
{
    public class AddressForUpdateDto
    {
        public int Id { get; set; }
        public string AddressDetails { get; set; }
        public int CityId { get; set; }
    }
}
