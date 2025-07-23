using System.Text.Json.Serialization;

namespace AmazonApiServer.Models
{
    public class ProductDisplay
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("product")]
        public Product? Product { get; set; }
        [JsonPropertyName("image")]
        public required string Image { get; set; }
    }
}
