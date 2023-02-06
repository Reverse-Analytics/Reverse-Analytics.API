namespace ReverseAnalytics.Domain.DTOs.Address
{
    public class AddressForCreateDto
    {
        public string AddressDetails { get; set; }
        public string? AddressLandMark { get; set; }
        public double? Longtitude { get; set; }
        public double? Latitude { get; set; }

        public int PersonId { get; set; }
    }
}
