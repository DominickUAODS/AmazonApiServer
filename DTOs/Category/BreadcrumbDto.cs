using System.Text.Json.Serialization;

namespace AmazonApiServer.DTOs.Category
{
	public class BreadcrumbDto
	{
		[JsonPropertyName("id")]
		public Guid Id { get; set; }
		[JsonPropertyName("name")]
		public string Name { get; set; } = string.Empty;
	}
}
