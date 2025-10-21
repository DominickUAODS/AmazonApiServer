using System.Text.Json.Serialization;

namespace AmazonApiServer.Filters
{
	public class UsersFilter
	{
		[JsonPropertyName("user")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Guid? UserId { get; set; }

		[JsonPropertyName("search")]
		public string? Search { get; set; }

		[JsonPropertyName("role")]
		public string? Role { get; set; }

		[JsonPropertyName("page")]
		public int Page { get; set; } = 1;

		[JsonPropertyName("page_size")]
		public int PageSize { get; set; } = 10;
	}
}
