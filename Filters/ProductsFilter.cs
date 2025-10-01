using System.Text.Json.Serialization;

namespace AmazonApiServer.Filters
{
	public class ProductsFilter
	{
		[JsonPropertyName("category")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Guid? CategoryId { get; set; }

		[JsonPropertyName("include_reviews")]
		public bool IncludeReviews { get; set; } = true;

		[JsonPropertyName("only_discounted")]
		public bool OnlyDiscounted { get; set; } = false;

		[JsonPropertyName("min_price")]
		public double? MinPrice { get; set; }

		[JsonPropertyName("max_price")]
		public double? MaxPrice { get; set; }

		[JsonPropertyName("ratings")]
		public List<int>? Ratings { get; set; }

		[JsonPropertyName("page")]
		public int Page { get; set; } = 1;

		[JsonPropertyName("page_size")]
		public int PageSize { get; set; } = 12;

		[JsonPropertyName("trending_days")]
		public int? TrendingDays { get; set; }
	}
}