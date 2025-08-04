using AmazonApiServer.Attributes;
using AmazonApiServer.Enums;
using System.Text.Json.Serialization;

namespace AmazonApiServer.DTOs.Category
{
    public class EditCategoryDto
    {
        [JsonPropertyName("image")]
        [FileExtensions("jpg")]
        public required IFormFile Image { get; set; }
        [JsonPropertyName("icon")]
        public CategoryIcon Icon { get; set; }
        [JsonPropertyName("name")]
        public required string Name { get; set; }
        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; }
        [JsonPropertyName("description")]
        public required string Description { get; set; }
        [JsonPropertyName("parent_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Guid? ParentId { get; set; }
    }
}
