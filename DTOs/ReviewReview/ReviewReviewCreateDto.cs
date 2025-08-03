namespace AmazonApiServer.DTOs.ReviewReview
{
	public class ReviewReviewCreateDto
	{
		public Guid ReviewId { get; set; }
		public Guid UserId { get; set; }
		public bool IsHelpful { get; set; }
	}
}
