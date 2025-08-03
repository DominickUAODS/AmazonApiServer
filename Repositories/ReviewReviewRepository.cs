using AmazonApiServer.Data;
using AmazonApiServer.DTOs.ReviewReview;
using AmazonApiServer.Interfaces;
using AmazonApiServer.Models;

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
				.Select(rr => new ReviewReviewDto
				{
					Id = rr.Id,
					ReviewId = rr.ReviewId,
					UserId = rr.UserId,
					IsHelpful = rr.IsHelpful
				}).ToListAsync();
		}

		public async Task<ReviewReviewDto?> GetByIdAsync(Guid id)
		{
			var rr = await _context.ReviewReviews.FindAsync(id);
			if (rr == null) return null;

			return new ReviewReviewDto
			{
				Id = rr.Id,
				ReviewId = rr.ReviewId,
				UserId = rr.UserId,
				IsHelpful = rr.IsHelpful
			};
		}

		public async Task<ReviewReviewDto?> CreateAsync(ReviewReviewCreateDto dto)
		{
			var rr = new ReviewReview
			{
				Id = Guid.NewGuid(),
				ReviewId = dto.ReviewId,
				UserId = dto.UserId,
				IsHelpful = dto.IsHelpful
			};

			_context.ReviewReviews.Add(rr);
			await _context.SaveChangesAsync();
			return await GetByIdAsync(rr.Id);
		}

		public async Task<ReviewReviewDto?> UpdateAsync(ReviewReviewUpdateDto dto)
		{
			var rr = await _context.ReviewReviews.FindAsync(dto.Id);
			if (rr == null) return null;

			rr.IsHelpful = dto.IsHelpful;
			await _context.SaveChangesAsync();
			return await GetByIdAsync(rr.Id);
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var rr = await _context.ReviewReviews.FindAsync(id);
			if (rr == null) return false;

			_context.ReviewReviews.Remove(rr);
			await _context.SaveChangesAsync();
			return true;
		}
	}

}
