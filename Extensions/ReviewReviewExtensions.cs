using AmazonApiServer.DTOs.ReviewReview;
using AmazonApiServer.Models;

namespace AmazonApiServer.Extensions
{
	public static class ReviewReviewExtensions
	{
		public static ReviewReviewDto ToDto(this ReviewReview reviewReview)
		{
			return new ReviewReviewDto
			{
				Id = reviewReview.Id,
				UserId = reviewReview.UserId,
				ReviewId = reviewReview.ReviewId,
				IsHelpful = reviewReview.IsHelpful
			};
		}

		public static ReviewReview ToEntity(this ReviewReviewCreateDto dto)
		{
			return new ReviewReview
			{
				Id = Guid.NewGuid(),
				UserId = dto.UserId,
				ReviewId = dto.ReviewId,
				IsHelpful = dto.IsHelpful
			};
		}

		public static void UpdateFromDto(this ReviewReview reviewReview, ReviewReviewUpdateDto dto)
		{
			reviewReview.IsHelpful = dto.IsHelpful;
		}
	}
}
