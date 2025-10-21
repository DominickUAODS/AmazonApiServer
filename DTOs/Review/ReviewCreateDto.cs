using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AmazonApiServer.Enums;

namespace AmazonApiServer.DTOs.Review
{
	public class ReviewCreateDto
	{
		[Required]
		[JsonPropertyName("user_id")]
		public Guid UserId { get; set; }

		[Required]
		[JsonPropertyName("product_id")]
		public Guid ProductId { get; set; }

		[Required]
		[JsonPropertyName("stars")]
		public int Stars { get; set; }

		[Required]
		[JsonPropertyName("title")]
		public string Title { get; set; } = string.Empty;

		[Required]
		[JsonPropertyName("content")]
		public string Content { get; set; } = string.Empty;

		[Required]
		[JsonPropertyName("published")]
		public DateTime Published { get; set; }

		[JsonPropertyName("rewiew_tags")]
		public List<ProductReviewTag>? ReviewTags { get; set; }

		[JsonPropertyName("rewiew_images")]
		public List<string>? ReviewImages { get; set; }
	}
}