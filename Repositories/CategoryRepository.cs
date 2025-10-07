using AmazonApiServer.Data;
using AmazonApiServer.DTOs.Category;
using AmazonApiServer.Filters;
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
			return category; // todo maybe include foreign keys for debug purposes
		}

		public async Task<Category?> DeleteAsync(Guid categoryId)
		{
			Category? category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
			if (category is not null)
			{
				_context.Categories.Remove(category);
				await _context.SaveChangesAsync();
			}
			return category; // todo maybe include foreign keys for debug purposes
		}

		public async Task<Category?> EditAsync(Category category)
		{
			//Category? existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);
			//if (existingCategory is not null)
			//{
			//	existingCategory.Image = category.Image;
			//	existingCategory.Name = category.Name;
			//	existingCategory.Description = category.Description;
			//	existingCategory.Icon = category.Icon;
			//	existingCategory.IsActive = category.IsActive;
			//	existingCategory.ParentId = category.ParentId;
			//	existingCategory.PropertyKeys = category.PropertyKeys;
			//	await _context.SaveChangesAsync();
			//}
			//return existingCategory; // todo maybe include foreign keys for debug purposes

			var existingCategory = await _context.Categories
				.Include(c => c.PropertyKeys) // загружаем дочерние элементы
				.FirstOrDefaultAsync(c => c.Id == category.Id);

			if (existingCategory is null)
				return null;

			// обновляем основные поля
			existingCategory.Image = category.Image;
			existingCategory.Name = category.Name;
			existingCategory.Description = category.Description;
			existingCategory.Icon = category.Icon;
			existingCategory.IsActive = category.IsActive;
			existingCategory.ParentId = category.ParentId;

			// удаляем все старые PropertyKeys
			_context.PropertyKeys.RemoveRange(existingCategory.PropertyKeys);

			// добавляем новые, если есть
			if (category.PropertyKeys != null && category.PropertyKeys.Any())
			{
				var newKeys = category.PropertyKeys
					.Select(pk => new PropertyKey { Name = pk.Name, CategoryId = existingCategory.Id })
					.ToList();

				existingCategory.PropertyKeys = newKeys;
			}

			await _context.SaveChangesAsync();
			return existingCategory;
		}

		public async Task<List<Category>> GetAllAsync(CategoriesFilter filter)
		{
			IQueryable<Category> query = _context.Categories;

			// Фильтр по родителю
			if (filter.ParentId.HasValue)
				query = query.Where(c => c.ParentId == filter.ParentId.Value);

			// Фильтр по активности
			if (filter.IsActive.HasValue)
				query = query.Where(c => c.IsActive == filter.IsActive.Value);

			// Фильтр на родителя
			if (filter.IsParent.HasValue)
				query = query.Where(c => c.ParentId == null);

			// Поиск по имени (частичное совпадение)
			if (!string.IsNullOrWhiteSpace(filter.Name))
				query = query.Where(c => c.Name.Contains(filter.Name));

			// Категории с товарами / пустые
			if (filter.HasProducts.HasValue)
			{
				if (filter.HasProducts.Value)
					query = query.Where(c => c.Products != null && c.Products.Any());
				else
					query = query.Where(c => c.Products == null || !c.Products.Any());
			}

			// Пагинация
			//query = query
			//	.Skip((filter.Page - 1) * filter.PageSize)
			//	.Take(filter.PageSize);

			return await query.ToListAsync();
		}

		public async Task<Category?> GetByIdAsync(Guid id)
		{
			return await _context.Categories.Include(e => e.PropertyKeys).FirstOrDefaultAsync(c => c.Id == id);
		}


		public async Task<List<Category>?> SearchCategoriesAsync(string? query)
		{
			if (!string.IsNullOrWhiteSpace(query))
			{
				var q = query.ToLower();
				var categories = await _context.Categories.Where(c => c.Name.ToLower().Contains(q)).ToListAsync();
				return categories;
			}
			return null;
		}

		public async Task<List<BreadcrumbDto>> GetBreadcrumbsAsync(Guid categoryId)
		{
			var breadcrumbs = new List<BreadcrumbDto>();
			var category = await _context.Categories.FindAsync(categoryId);

			while (category != null)
			{
				breadcrumbs.Insert(0, new BreadcrumbDto
				{
					Id = category.Id,
					Name = category.Name
				});

				if (category.ParentId == null) break;
				category = await _context.Categories.FindAsync(category.ParentId);
			}

			return breadcrumbs;
		}
	}
}