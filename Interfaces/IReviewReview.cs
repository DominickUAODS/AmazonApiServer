using AmazonApiServer.DTOs.ReviewReview;

namespace AmazonApiServer.Interfaces
{
	public interface IReviewReview
	{
		Task<IEnumerable<ReviewReviewDto>> GetAllAsync();
		Task<ReviewReviewDto?> GetByIdAsync(Guid id);
		Task<ReviewReviewDto?> CreateAsync(ReviewReviewCreateDto dto);
		Task<ReviewReviewDto?> UpdateAsync(ReviewReviewUpdateDto dto);
		Task<bool> DeleteAsync(Guid id);
	}
}
