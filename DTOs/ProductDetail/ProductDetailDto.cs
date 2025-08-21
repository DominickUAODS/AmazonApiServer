using System.Text.Json.Serialization;

namespace AmazonApiServer.DTOs.ProductDetail
{
    public class ProductDetailDto
    {
        [JsonPropertyName("property_key")]
        public required string PropertyKey { get; set; }
        [JsonPropertyName("attribute")]
        public required string Attribute { get; set; }
    }
}
