using AmazonApiServer.Data;
using AmazonApiServer.DTOs.Review;
using AmazonApiServer.Interfaces;
using AmazonApiServer.Models;
using Microsoft.EntityFrameworkCore;

namespace AmazonApiServer.Repositories
{
	public class ReviewRepository : IReview
	{
		private readonly ApplicationContext _context;

		public ReviewRepository(ApplicationContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<ReviewDto>> GetAllAsync()
		{
			return await _context.Reviews
				.Select(r => new ReviewDto
				{
					Id = r.Id,
					UserId = r.UserId,
					ProductId = r.ProductId,
					Stars = r.Stars,
					Title = r.Title,
					Content = r.Content,
					Published = r.Published
				}).ToListAsync();
		}

		public async Task<ReviewDto?> GetByIdAsync(Guid id)
		{
			var r = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
			if (r == null) return null;

			return new ReviewDto
			{
				Id = r.Id,
				UserId = r.UserId,
				ProductId = r.ProductId,
				Stars = r.Stars,
				Title = r.Title,
				Content = r.Content,
				Published = r.Published
			};
		}

		public async Task<ReviewDto?> CreateAsync(ReviewCreateDto dto)
		{
			var review = new Review
			{
				Id = Guid.NewGuid(),
				UserId = dto.UserId,
				ProductId = dto.ProductId,
				Stars = dto.Stars,
				Title = dto.Title,
				Content = dto.Content,
				Published = DateTime.UtcNow
			};

			_context.Reviews.Add(review);
			await _context.SaveChangesAsync();

			return await GetByIdAsync(review.Id);
		}

		public async Task<ReviewDto?> UpdateAsync(ReviewUpdateDto dto)
		{
			var r = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == dto.Id);
			if (r == null) return null;

			r.Stars = dto.Stars;
			r.Title = dto.Title;
			r.Content = dto.Content;

			await _context.SaveChangesAsync();
			return await GetByIdAsync(r.Id);
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var r = await _context.Reviews.FindAsync(id);
			if (r == null) return false;

			_context.Reviews.Remove(r);
			await _context.SaveChangesAsync();
			return true;
		}
	}

}
