using AmazonApiServer.Enums;
using System.Text.Json.Serialization;

namespace AmazonApiServer.Models
{
    public class Category
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("image")]
        public required string Image { get; set; }
        [JsonPropertyName("icon")]
        public CategoryIcon Icon { get; set; }
        [JsonPropertyName("name")]
        public required string Name { get; set; }
        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; }
        [JsonPropertyName("description")]
        public required string Description { get; set; }
        [JsonPropertyName("parent")]
        public Category? Parent { get; set; }
        [JsonPropertyName("products")]
        public List<Product>? Products { get; set; }
        [JsonPropertyName("property_keys")]
        public List<PropertyKey>? PropertyKeys { get; set; }
    }
}
