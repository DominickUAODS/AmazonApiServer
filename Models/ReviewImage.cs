using System.Text.Json.Serialization;

namespace AmazonApiServer.Models
{
    public class ReviewImage
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("review")]
        public Review? Review { get; set; }
        [JsonPropertyName("image")]
        public required string Image { get; set; }
    }
}
