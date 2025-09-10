using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AmazonApiServer.DTOs.ReviewReview
{
	public class ReviewReviewCreateDto
	{
		[Required]
		[JsonPropertyName("review_id")]
		public Guid ReviewId { get; set; }

		[Required]
		[JsonPropertyName("user_id")]
		public Guid UserId { get; set; }
		
		[JsonPropertyName("is_helpful")]
		public bool IsHelpful { get; set; }
	}
}