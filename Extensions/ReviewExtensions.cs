using AmazonApiServer.DTOs.Review;
using AmazonApiServer.Models;

namespace AmazonApiServer.Extensions
{
	public static class ReviewExtensions
	{
		public static ReviewDto ToDto(this Review review)
		{
			return new ReviewDto
			{
				Id = review.Id,
				UserId = review.UserId,
				ProductId = review.ProductId,
				Title = review.Title,
				Content = review.Content,
				Stars = review.Stars,
				Published = review.Published
			};
		}

		public static Review ToEntity(this ReviewCreateDto dto)
		{
			return new Review
			{
				Id = Guid.NewGuid(),
				UserId = dto.UserId,
				ProductId = dto.ProductId,
				Title = dto.Title,
				Content = dto.Content,
				Stars = dto.Stars,
				Published = dto.Published
			};
		}

		public static void UpdateFromDto(this Review review, ReviewUpdateDto dto)
		{
			review.Title = dto.Title;
			review.Content = dto.Content;
			review.Stars = dto.Stars;
			review.Published = dto.Published;
		}
	}
}
