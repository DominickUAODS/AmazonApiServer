using System.Text.Json.Serialization;

namespace AmazonApiServer.DTOs.ReviewReview
{
	public class ReviewReviewDto
	{
		[JsonPropertyName("id")]
		public Guid Id { get; set; }
		
		[JsonPropertyName("review_id")]
		public Guid ReviewId { get; set; }
		
		[JsonPropertyName("user_id")]
		public Guid UserId { get; set; }

		[JsonPropertyName("is_helpful")]
		public bool IsHelpful { get; set; }

		[JsonPropertyName("count")]
		public int Count { get; set; }
	}
}