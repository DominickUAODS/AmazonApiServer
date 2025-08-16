using System.Text.Json.Serialization;

namespace AmazonApiServer.DTOs.Product
{
    public class ProductInListDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public required string Name { get; set; }
        [JsonPropertyName("price")]
        public float Price { get; set; }
        [JsonPropertyName("discount")]
        public int? Discount { get; set; }
        [JsonPropertyName("display")]
        public required string Display { get; set; }
    }
}
