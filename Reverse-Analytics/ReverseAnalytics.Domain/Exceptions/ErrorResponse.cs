using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ReverseAnalytics.Domain.Exceptions
{
    public class ErrorResponse
    {
        public EventId EventId { get; set; }
        public int? StatusCode { get; set; }
        public string Message { get; set; }
        public string Path { get; set; }
        public string Method { get; set; }
        public List<string> Errors { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(new
            {
                StatusCode,
                Path,
                Method,
                Message,
                Errors
            });
        }
    }
}
