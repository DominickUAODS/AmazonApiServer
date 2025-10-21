using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AmazonApiServer.DTOs.ReviewReview
{
	public class ReviewReviewUpdateDto : ReviewReviewCreateDto
	{
		[Required]
		[JsonPropertyName("review_id")]
		public Guid Id { get; set; }
	}
}
