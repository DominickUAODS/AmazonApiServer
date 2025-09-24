using AmazonApiServer.Enums;
using System.Text.Json.Serialization;

namespace AmazonApiServer.DTOs.Category
{
	public class CategoryDto
	{
		[JsonPropertyName("id")]
		public Guid Id { get; set; }
		[JsonPropertyName("image")]
		public string Image { get; set; } = string.Empty;
		[JsonPropertyName("icon")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public CategoryIcon? Icon { get; set; }
		[JsonPropertyName("name")]
		public string Name { get; set; } = string.Empty;
		[JsonPropertyName("is_active")]
		public bool IsActive { get; set; }
		[JsonPropertyName("description")]
		public string Description { get; set; } = string.Empty;
		[JsonPropertyName("parent_id")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Guid? ParentId { get; set; }
		[JsonPropertyName("property_keys")]
		public List<string>? PropertyKeys { get; set; }
	}
}
