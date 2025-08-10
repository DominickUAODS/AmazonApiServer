using AmazonApiServer.Data;
using AmazonApiServer.Interfaces;
using AmazonApiServer.Models;
using Microsoft.EntityFrameworkCore;

namespace AmazonApiServer.Repositories
{
    public class CategoryRepository(ApplicationContext context) : ICategoryRepo
    {
        private readonly ApplicationContext _context = context;

        public async Task<Category> CreateAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> DeleteAsync(Guid categoryId)
        {
            Category? category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
            if (category is not null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
            return category;
        }

        public async Task<Category?> EditAsync(Category category)
        {
            Category? existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);
            if (existingCategory is not null)
            {
                existingCategory.Image = category.Image;
                existingCategory.Name = category.Name;
                existingCategory.Description = category.Description;
                existingCategory.Icon = category.Icon;
                existingCategory.IsActive = category.IsActive;
                existingCategory.Parent = category.Parent;
                existingCategory.PropertyKeys = category.PropertyKeys;
                await _context.SaveChangesAsync();
            }
            return existingCategory;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
