using System.Text.Json.Serialization;

namespace AmazonApiServer.Models
{
    public class ProductFeature
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public required string Name { get; set; }
        [JsonPropertyName("product")]
        public Product? Product { get; set; }
        [JsonPropertyName("description")]
        public required string Description { get; set; }
    }
}
