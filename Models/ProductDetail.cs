using System.Text.Json.Serialization;

namespace AmazonApiServer.Models
{
    public class ProductDetail
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("property_key")]
        public required string PropertyKey { get; set; }
        [JsonPropertyName("product")]
        public Product? Product { get; set; }
        [JsonPropertyName("attribute")]
        public required string Attribute { get; set; }
    }
}
