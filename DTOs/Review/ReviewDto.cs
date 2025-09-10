using AmazonApiServer.Enums;
using System.Text.Json.Serialization;

namespace AmazonApiServer.DTOs.Review
{
	public class ReviewDto
	{
		[JsonPropertyName("id")]
		public Guid Id { get; set; }

		[JsonPropertyName("user_image")]
		public string UserImage { get; set; } = string.Empty;

		[JsonPropertyName("user_firstname")]
		public string UserFirstName { get; set; } = string.Empty;

		[JsonPropertyName("user_lastname")]
		public string UserLastName { get; set; } = string.Empty;

		[JsonPropertyName("product_id")]
		public Guid ProductId { get; set; }

		[JsonPropertyName("stars")]
		public int Stars { get; set; }

		[JsonPropertyName("title")]
		public string Title { get; set; } = string.Empty;

		[JsonPropertyName("content")]
		public string Content { get; set; } = string.Empty;

		[JsonPropertyName("published")]
		public DateTime Published { get; set; }

		[JsonPropertyName("helpful_count")]
		public int HelpfulCount { get; set; }

		[JsonPropertyName("rewiew_tags")]
		public List<ProductReviewTag>? ReviewTags { get; set; }

		[JsonPropertyName("rewiew_images")]
		public List<string>? ReviewImages { get; set; }
	}
}