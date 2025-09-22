using AmazonApiServer.DTOs.Review;
using AmazonApiServer.DTOs.ReviewReview;

namespace AmazonApiServer.Interfaces
{
	public interface IReview
	{
		Task<IEnumerable<ReviewDto>> GetAllAsync();
		Task<ReviewDto?> GetByIdAsync(Guid id);
		Task<IEnumerable<ReviewDto?>?> GetByProductIdAsync(Guid productId, Guid? currentUserId, int? stars, string[]? tags, string filterMode, string sort, int skip, int take);
		Task<IEnumerable<ReviewDto?>?> GetByUserIdAsync(Guid id);
		Task<ReviewDto?> CreateAsync(ReviewCreateDto dto);
		Task<ReviewDto?> UpdateAsync(ReviewUpdateDto dto);
		Task<bool> DeleteAsync(Guid id);
		Task<ReviewInfoDto> GetReviewInfoAsync(Guid productId);
	}
}
