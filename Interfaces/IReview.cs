using AmazonApiServer.DTOs.Review;

namespace AmazonApiServer.Interfaces
{
	public interface IReview
	{
		Task<IEnumerable<ReviewDto>> GetAllAsync();
		Task<ReviewDto?> GetByIdAsync(Guid id);
		Task<ReviewDto?> CreateAsync(ReviewCreateDto dto);
		Task<ReviewDto?> UpdateAsync(ReviewUpdateDto dto);
		Task<bool> DeleteAsync(Guid id);
	}
}
