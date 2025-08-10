using AmazonApiServer.Attributes;
using AmazonApiServer.DTOs.ProductDetail;
using AmazonApiServer.DTOs.ProductFeature;
using System.Text.Json.Serialization;

namespace AmazonApiServer.DTOs.Product
{
    public class AddProductDto
    {
        [JsonPropertyName("code")]
        public required string Code { get; set; }
        [JsonPropertyName("category_id")]
        public Guid? CategoryId { get; set; }
        [JsonPropertyName("price")]
        public float Price { get; set; }
        [JsonPropertyName("discount")]
        public int? Discount { get; set; }
        [JsonPropertyName("number")]
        public int Number { get; set; }
        [JsonPropertyName("displays")]
        [FileExtensions("jpg,png")]
        public required IFormFileCollection Displays { get; set; }
        [JsonPropertyName("product_details")]
        public List<ProductDetailDto>? ProductDetails { get; set; }
        [JsonPropertyName("product_features")]
        public List<ProductFeatureDto>? ProductFeatures { get; set; }
    }
}
