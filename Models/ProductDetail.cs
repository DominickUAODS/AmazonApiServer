using System.Text.Json.Serialization;

namespace AmazonApiServer.Models
{
	public class ProductDetail
	{
		[JsonPropertyName("id")]
		public Guid Id { get; set; }
		[JsonPropertyName("product_id")]
		public Guid ProductId { get; set; }
		[JsonPropertyName("property_key_id")]
		public Guid PropertyKeyId { get; set; }
		[JsonIgnore]
		public PropertyKey PropertyKey { get; set; } = null!;
		[JsonPropertyName("product")]
		public Product Product { get; set; } = null!;
		[JsonPropertyName("attribute")]
		public required string Attribute { get; set; }
	}
}
