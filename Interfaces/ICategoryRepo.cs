using AmazonApiServer.DTOs.Category;
using AmazonApiServer.Filters;
using AmazonApiServer.Models;

namespace AmazonApiServer.Interfaces
{
    public interface ICategoryRepo
    {
        Task<Category> CreateAsync(Category category);
        Task<Category?> EditAsync(Category category);
        Task<Category?> DeleteAsync(Guid categoryId);
        Task<List<Category>> GetAllAsync(CategoriesFilter filter);
        Task<Category?> GetByIdAsync(Guid id);
        Task<List<Category>?> SearchCategoriesAsync(string? query);
        Task<List<BreadcrumbDto>> GetBreadcrumbsAsync(Guid categoryId);
	}
}
