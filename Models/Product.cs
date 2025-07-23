using System.Text.Json.Serialization;

namespace AmazonApiServer.Models
{
    public class Product
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("code")]
        public required string Code { get; set; }
        [JsonPropertyName("category")]
        public Category? Category { get; set; }
        [JsonPropertyName("price")]
        public float Price { get; set; }
        [JsonPropertyName("discount")]
        public int? Discount { get; set; }
        [JsonPropertyName("number")]
        public int Number { get; set; }
        [JsonPropertyName("wishlisted_by")]
        public List<User>? WishlistedBy { get; set; }
        [JsonPropertyName("reviews")]
        public List<Review>? Reviews { get; set; }
        [JsonPropertyName("displays")]
        public List<ProductDisplay>? Displays { get; set; }
        [JsonPropertyName("details")]
        public List<ProductDetail>? Details { get; set; }
        [JsonPropertyName("features")]
        public List<ProductFeature>? Features { get; set; }
        [JsonPropertyName("order_items")]
        public List<OrderItem>? OrderItems { get; set; }
    }
}
