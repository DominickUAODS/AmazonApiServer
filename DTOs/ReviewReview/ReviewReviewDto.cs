namespace AmazonApiServer.DTOs.ReviewReview
{
	public class ReviewReviewDto
	{
		public Guid Id { get; set; }
		public Guid ReviewId { get; set; }
		public Guid UserId { get; set; }
		public bool IsHelpful { get; set; }
	}

}
