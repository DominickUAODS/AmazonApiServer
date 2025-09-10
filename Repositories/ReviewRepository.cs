using AmazonApiServer.Data;
using AmazonApiServer.DTOs.Review;
using AmazonApiServer.Enums;
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
			var reviews = await _context.Reviews
				.AsNoTracking()
				.Include(r => r.User)
				.Include(r => r.ReviewTags)
				.Include(rt => rt.ReviewReviews)
				.Include(r => r.ReviewImages)
				.OrderBy(r => r.Published)
				.ToListAsync();

			var dtos = reviews.Select(r => new ReviewDto
			{
				Id = r.Id,
				UserImage = r.User?.ProfilePhoto ?? string.Empty,
				UserFirstName = r.User?.FirstName ?? string.Empty,
				UserLastName = r.User?.LastName ?? string.Empty,
				ProductId = r.ProductId,
				Stars = r.Stars,
				Title = r.Title,
				Content = r.Content,
				Published = r.Published,

				HelpfulCount = r.ReviewTags?.Count ?? 0,

				ReviewTags = r.ReviewTags != null
					? r.ReviewTags.Select(rt => rt.Tag).ToList()
					: new List<ProductReviewTag>(),

				ReviewImages = r.ReviewImages != null
					? r.ReviewImages.Select(ri => ri.Image).ToList()
					: new List<string>()
			}).ToList();

			return dtos;
		}

		public async Task<ReviewDto?> GetByIdAsync(Guid id)
		{
			var r = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
			if (r == null) return null;

			return new ReviewDto
			{
				Id = r.Id,
				UserImage = r.User?.ProfilePhoto ?? string.Empty,
				UserFirstName = r.User?.FirstName ?? string.Empty,
				UserLastName = r.User?.LastName ?? string.Empty,
				ProductId = r.ProductId,
				Stars = r.Stars,
				Title = r.Title,
				Content = r.Content,
				Published = r.Published,

				HelpfulCount = r.ReviewTags?.Count ?? 0,

				ReviewTags = r.ReviewTags != null
					? r.ReviewTags.Select(rt => rt.Tag).ToList()
					: new List<ProductReviewTag>(),

				ReviewImages = r.ReviewImages != null
					? r.ReviewImages.Select(ri => ri.Image).ToList()
					: new List<string>()
			};
		}

		public async Task<IEnumerable<ReviewDto?>?> GetByProductIdAsync(Guid id)
		{
			var reviews = await _context.Reviews
				.AsNoTracking()
				.Include(r => r.User)
				.Include(r => r.ReviewTags)
				.Include(r => r.ReviewReviews)
				.Include(r => r.ReviewImages)
				.Where(r => r.ProductId == id)
				.OrderBy(r => r.Published)
				.ToListAsync();

			if (reviews.Count == 0)
				return Enumerable.Empty<ReviewDto>();

			var dtos = reviews.Select(r => new ReviewDto
			{
				Id = r.Id,
				UserImage = r.User?.ProfilePhoto ?? string.Empty,
				UserFirstName = r.User?.FirstName ?? string.Empty,
				UserLastName = r.User?.LastName ?? string.Empty,
				ProductId = r.ProductId,
				Stars = r.Stars,
				Title = r.Title,
				Content = r.Content,
				Published = r.Published,

				HelpfulCount = r.ReviewTags?.Count ?? 0,

				ReviewTags = r.ReviewTags != null
					? r.ReviewTags.Select(rt => rt.Tag).ToList()
					: new List<ProductReviewTag>(),

				ReviewImages = r.ReviewImages != null
					? r.ReviewImages.Select(ri => ri.Image).ToList()
					: new List<string>()
			}).ToList();

			return dtos;
		}

		public async Task<IEnumerable<ReviewDto?>?> GetByUserIdAsync(Guid id)
		{
			var reviews = await _context.Reviews
				.AsNoTracking()
				.Include(r => r.User)
				.Include(r => r.ReviewTags)
				.Include(r => r.ReviewReviews)
				.Include(r => r.ReviewImages)
				.Where(r => r.UserId == id)
				.OrderBy(r => r.Published)
				.ToListAsync();

			if (reviews.Count == 0)
				return Enumerable.Empty<ReviewDto>();

			var dtos = reviews.Select(r => new ReviewDto
			{
				Id = r.Id,
				UserImage = r.User?.ProfilePhoto ?? string.Empty,
				UserFirstName = r.User?.FirstName ?? string.Empty,
				UserLastName = r.User?.LastName ?? string.Empty,
				ProductId = r.ProductId,
				Stars = r.Stars,
				Title = r.Title,
				Content = r.Content,
				Published = r.Published,

				HelpfulCount = r.ReviewTags?.Count ?? 0,

				ReviewTags = r.ReviewTags != null
					? r.ReviewTags.Select(rt => rt.Tag).ToList()
					: new List<ProductReviewTag>(),

				ReviewImages = r.ReviewImages != null
					? r.ReviewImages.Select(ri => ri.Image).ToList()
					: new List<string>()
			}).ToList();

			return dtos;
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

			if (dto.ReviewTags != null)
			{
				foreach (var reviewTag in dto.ReviewTags)
				{
					_context.ReviewTags.Add(new ReviewTag
					{
						Id = Guid.NewGuid(),
						ReviewId = review.Id,
						Tag = reviewTag,

					});
				}
			}

			if (dto.ReviewImages != null)
			{
				foreach (var reviewImage in dto.ReviewImages)
				{
					_context.ReviewImages.Add(new ReviewImage
					{
						Id = Guid.NewGuid(),
						ReviewId = review.Id,
						Image = reviewImage,

					});
				}
			}

			await _context.SaveChangesAsync();
			return await GetByIdAsync(review.Id);
		}

		public async Task<ReviewDto?> UpdateAsync(ReviewUpdateDto dto)
		{
			var review = await _context.Reviews
				.Include(r => r.ReviewTags)
				.Include(r => r.ReviewImages)
				.FirstOrDefaultAsync(r => r.Id == dto.Id);

			if (review == null) return null;

			review.Stars = dto.Stars;
			review.Title = dto.Title;
			review.Content = dto.Content;

			if (dto.ReviewTags != null)
			{
				_context.ReviewTags.RemoveRange(review.ReviewTags);

				foreach (var tag in dto.ReviewTags)
				{
					review.ReviewTags.Add(new ReviewTag
					{
						Id = Guid.NewGuid(),
						ReviewId = review.Id,
						Tag = tag
					});
				}
			}

			if (dto.ReviewImages != null)
			{
				_context.ReviewImages.RemoveRange(review.ReviewImages);

				foreach (var img in dto.ReviewImages)
				{
					review.ReviewImages.Add(new ReviewImage
					{
						Id = Guid.NewGuid(),
						ReviewId = review.Id,
						Image = img
					});
				}
			}

			await _context.SaveChangesAsync();
			return await GetByIdAsync(review.Id);
		}



		public async Task<bool> DeleteAsync(Guid id)
		{
			var review = await _context.Reviews.FindAsync(id);
			if (review == null) return false;

			_context.Reviews.Remove(review);
			await _context.SaveChangesAsync();
			return true;
		}
	}

}
