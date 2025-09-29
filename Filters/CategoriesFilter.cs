using System.Text.Json.Serialization;

namespace AmazonApiServer.Filters
{
	public class CategoriesFilter
	{
		[JsonPropertyName("parent_id")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Guid? ParentId { get; set; }   // фильтр по родителю (например, взять только дочерние категории)

		[JsonPropertyName("is_active")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public bool? IsActive { get; set; }   // активные/неактивные категории

		[JsonPropertyName("is_parent")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public bool? IsParent { get; set; }   // получаем родителей

		[JsonPropertyName("name")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string? Name { get; set; }     // поиск по названию (contains)

		[JsonPropertyName("has_products")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public bool? HasProducts { get; set; } // фильтр — категории с товарами или пустые

		[JsonPropertyName("page")]
		public int Page { get; set; } = 1;    // пагинация

		[JsonPropertyName("page_size")]
		public int PageSize { get; set; } = 6;
	}
}
