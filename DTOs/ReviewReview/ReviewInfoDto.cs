using System.Text.Json.Serialization;

namespace AmazonApiServer.DTOs.ReviewReview
{
	public class ReviewInfoDto
	{
		[JsonPropertyName("average")]
		public int Average { get; set; }
		[JsonPropertyName("total_reviews")]
		public int TotalReviews { get; set; }
		[JsonPropertyName("rating")]
		public int[] Ratings { get; set; } = new int[5];
	}
}
