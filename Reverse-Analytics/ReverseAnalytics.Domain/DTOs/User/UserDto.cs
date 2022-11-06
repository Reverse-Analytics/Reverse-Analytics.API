using System.Text.Json.Serialization;

namespace ReverseAnalytics.Domain.DTOs.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
    }
}
