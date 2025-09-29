using System.Text.Json.Serialization;

namespace AmazonApiServer.DTOs.ProductDetail
{
	public class ProductDetailDto
	{
		[JsonPropertyName("property_key_id")]
		public Guid PropertyKeyId { get; set; }

		[JsonPropertyName("property_key")]
		public string PropertyKey { get; set; } = string.Empty;

		[JsonPropertyName("attribute")]
		public string Attribute { get; set; } = string.Empty;
	}
}
