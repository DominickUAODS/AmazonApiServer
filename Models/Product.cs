using System.Text.Json.Serialization;

namespace AmazonApiServer.Models
{
    public class Product
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public required string Name { get; set; }
        [JsonPropertyName("code")]
        public required string Code { get; set; }
        [JsonPropertyName("category_id")]
        public Guid? CategoryId { get; set; }
        [JsonIgnore]
        public Category? Category { get; set; }
        [JsonPropertyName("price")]
        public float Price { get; set; }
        [JsonPropertyName("discount")]
        public int? Discount { get; set; }
        [JsonPropertyName("number")]
        public int Number { get; set; }
        
        public List<User>? WishlistedBy { get; set; }
        [JsonIgnore]
        public List<Review>? Reviews { get; set; } = new List<Review>();
		[JsonIgnore]
        public List<ProductDisplay> Displays { get; set; } = new List<ProductDisplay>();
		[JsonIgnore]
        public ICollection<ProductDetail> Details { get; set; } = new List<ProductDetail>();
		[JsonIgnore]
        public List<ProductFeature> Features { get; set; } = new List<ProductFeature>();
		[JsonIgnore]
        public List<OrderItem>? OrderItems { get; set; }
	}
}
