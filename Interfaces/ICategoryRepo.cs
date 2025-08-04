using AmazonApiServer.Models;

namespace AmazonApiServer.Interfaces
{
    public interface ICategoryRepo
    {
        Task<Category> CreateAsync(Category category);
        Task<Category?> EditAsync(Category category);
        Task<Category?> DeleteAsync(Guid categoryId);
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(Guid id);
    }
}
