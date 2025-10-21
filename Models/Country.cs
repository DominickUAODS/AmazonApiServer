using System.Text.Json.Serialization;

namespace AmazonApiServer.Models
{
	public class Country
	{
		[JsonPropertyName("id")]
		public Guid Id { get; set; }
		[JsonPropertyName("name")]
		public string Name { get; set; } = string.Empty;
		[JsonPropertyName("code")]
		public string Code { get; set; } = string.Empty;
	}
}
