using AmazonApiServer.Enums;
using System.Text.Json.Serialization;

namespace AmazonApiServer.DTOs.Category
{
	public class CategoryInListDto
	{
		[JsonPropertyName("id")]
		public Guid Id { get; set; }

		[JsonPropertyName("icon")]
		//[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public CategoryIcon? Icon { get; set; }

		[JsonPropertyName("name")]
		public required string Name { get; set; }

		[JsonPropertyName("parent_id")]
		//[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Guid? ParentId { get; set; }

		[JsonPropertyName("is_active")]
		public bool IsActive { get; set; }

		[JsonPropertyName("description")]
		public required string Description { get; set; }

		[JsonPropertyName("image")]
		public required string Image { get; set; }
	}
}
