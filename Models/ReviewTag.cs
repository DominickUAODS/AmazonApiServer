using System.Text.Json.Serialization;

namespace AmazonApiServer.Models
{
    public class ReviewTag
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("review")]
        public Review? Review { get; set; }
        [JsonPropertyName("tag")]
        public Enums.ReviewTag Tag { get; set; }
    }
}
