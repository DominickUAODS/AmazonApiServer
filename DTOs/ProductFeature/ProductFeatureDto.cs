using System.Text.Json.Serialization;

namespace AmazonApiServer.DTOs.ProductFeature
{
    public class ProductFeatureDto
    {
        [JsonPropertyName("name")]
        public required string Name { get; set; }
        [JsonPropertyName("description")]
        public required string Description { get; set; }
    }
}
