using System.Text.Json.Serialization;

namespace AmazonApiServer.Models
{
    public class PropertyKey
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public required string Name { get; set; }
		public Guid? CategoryId { get; set; }
		[JsonPropertyName("category")]
        public Category? Category { get; set; }
		[JsonIgnore]
		public List<ProductDetail>? ProductDetails { get; set; }
	}
}