using AmazonApiServer.DTOs.ReviewReview;
using AmazonApiServer.Models;

namespace AmazonApiServer.Extensions
{
	public static class ReviewReviewExtensions
	{
		public static ReviewReviewDto ToDto(this ReviewReview rr)
		{
			return new ReviewReviewDto
			{
				Id = rr.Id,
				UserId = rr.UserId,
				ReviewId = rr.ReviewId,
				IsHelpful = rr.IsHelpful
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

		public static void UpdateFromDto(this ReviewReview rr, ReviewReviewUpdateDto dto)
		{
			rr.IsHelpful = dto.IsHelpful;
		}
	}
}
