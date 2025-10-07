using System.Text.Json.Serialization;

namespace AmazonApiServer.DTOs.Category
{
	public class PropertyKeyDto
	{
		[JsonPropertyName("property_key_id")]
		public Guid Id { get; set; }
		[JsonPropertyName("property_key")]
		public string Name { get; set; } = string.Empty;
	}
}
