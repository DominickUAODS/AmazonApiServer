using AmazonApiServer.Enums;
using System.Text.Json.Serialization;

namespace AmazonApiServer.Models
{
	public class Category
	{
		[JsonPropertyName("id")]
		public Guid Id { get; set; }
		[JsonPropertyName("image")]
		public required string Image { get; set; }
		[JsonPropertyName("icon")]
		public CategoryIcon? Icon { get; set; }
		[JsonPropertyName("name")]
		public required string Name { get; set; }
		[JsonPropertyName("is_active")]
		public bool IsActive { get; set; }
		[JsonPropertyName("description")]
		public required string Description { get; set; }
		[JsonPropertyName("parent_id")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Guid? ParentId { get; set; }
		[JsonIgnore]
		public Category? Parent { get; set; }
		[JsonIgnore]
		public List<Category>? Children { get; set; }
		[JsonIgnore]
		public List<Product>? Products { get; set; }
		[JsonIgnore]
		public List<PropertyKey>? PropertyKeys { get; set; }
	}
}
