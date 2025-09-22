using System.Text.Json.Serialization;
using AmazonApiServer.DTOs.Category;
using AmazonApiServer.DTOs.ProductDetail;
using AmazonApiServer.DTOs.ProductFeature;
using AmazonApiServer.DTOs.Review;

namespace AmazonApiServer.DTOs.Product
{
    public class ProductDto
    {
		[JsonPropertyName("id")]
		public Guid Id { get; set; }
		[JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;
        [JsonPropertyName("category_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Guid? CategoryId { get; set; }
        [JsonPropertyName("price")]
        public float Price { get; set; }
        [JsonPropertyName("discount")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Discount { get; set; }
        [JsonPropertyName("number")]
        public int Number { get; set; }
        [JsonPropertyName("reviews")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<ReviewDto>? Reviews { get; set; }
        [JsonPropertyName("displays")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? Displays { get; set; }
        [JsonPropertyName("details")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<ProductDetailDto>? Details { get; set; }
        [JsonPropertyName("features")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<ProductFeatureDto>? Features { get; set; }

		[JsonPropertyName("stars")]
		public int? Stars { get; set; }

        [JsonPropertyName("comments")]
        public int? Comments { get; set; }

		[JsonPropertyName("image")]
		public string? Image { get; set; }

		[JsonPropertyName("category")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public CategoryDto? Category { get; set; }
	}
}
