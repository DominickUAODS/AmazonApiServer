using AmazonApiServer.Data;
using AmazonApiServer.DTOs.ReviewReview;
using AmazonApiServer.Interfaces;
using AmazonApiServer.Models;
using Microsoft.EntityFrameworkCore;

namespace AmazonApiServer.Repositories
{
	public class ReviewReviewRepository : IReviewReview
	{
		private readonly ApplicationContext _context;

		public ReviewReviewRepository(ApplicationContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<ReviewReviewDto>> GetAllAsync()
		{
			return await _context.ReviewReviews
				.Select(reviewReview => new ReviewReviewDto
				{
					Id = reviewReview.Id,
					ReviewId = reviewReview.ReviewId,
					UserId = reviewReview.UserId,
					IsHelpful = reviewReview.IsHelpful
				}).ToListAsync();
		}

		public async Task<ReviewReviewDto?> GetByIdAsync(Guid id)
		{
			var reviewReview = await _context.ReviewReviews.FindAsync(id);
			if (reviewReview == null) return null;

			return new ReviewReviewDto
			{
				Id = reviewReview.Id,
				ReviewId = reviewReview.ReviewId,
				UserId = reviewReview.UserId,
				IsHelpful = reviewReview.IsHelpful
			};
		}

		public async Task<ReviewReviewDto?> GetByReviewIdAsync(Guid id)
		{
			var reviewReviews = await _context.ReviewReviews.Where(rr => rr.ReviewId == id).ToListAsync();
			if (reviewReviews == null) return null;

			var helpfulCount = reviewReviews.Count(rr => rr.IsHelpful);

			return new ReviewReviewDto
			{
				ReviewId = id,
				Count = helpfulCount
			};
		}

		public async Task<ReviewReviewDto?> CreateAsync(ReviewReviewCreateDto dto)
		{
			var reviewReview = new ReviewReview
			{
				Id = Guid.NewGuid(),
				ReviewId = dto.ReviewId,
				UserId = dto.UserId,
				IsHelpful = dto.IsHelpful
			};

			_context.ReviewReviews.Add(reviewReview);
			await _context.SaveChangesAsync();
			return await GetByIdAsync(reviewReview.Id);
		}

		public async Task<ReviewReviewDto?> UpdateAsync(ReviewReviewUpdateDto dto)
		{
			var reviewReview = await _context.ReviewReviews.FindAsync(dto.Id);
			if (reviewReview == null) return null;

			reviewReview.IsHelpful = dto.IsHelpful;
			await _context.SaveChangesAsync();
			return await GetByIdAsync(reviewReview.Id);
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var reviewReview = await _context.ReviewReviews.FindAsync(id);
			if (reviewReview == null) return false;

			_context.ReviewReviews.Remove(reviewReview);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<ReviewReviewDto?> ToggleHelpfulAsync(ReviewReviewCreateDto dto)
		{
			var reviewReview = await _context.ReviewReviews.FirstOrDefaultAsync(r => r.ReviewId == dto.ReviewId && r.UserId == dto.UserId);

			if (reviewReview != null)
			{
				reviewReview.IsHelpful = dto.IsHelpful;
			}
			else
			{
				reviewReview = new ReviewReview
				{
					Id = Guid.NewGuid(),
					ReviewId = dto.ReviewId,
					UserId = dto.UserId,
					IsHelpful = dto.IsHelpful
				};

				_context.ReviewReviews.Add(reviewReview);
			}

			await _context.SaveChangesAsync();
			return await GetByReviewIdAsync(reviewReview.ReviewId);
		}
	}
}