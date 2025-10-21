using AmazonApiServer.Data;
using AmazonApiServer.Filters;
using AmazonApiServer.Interfaces;
using AmazonApiServer.Models;
using Microsoft.EntityFrameworkCore;

namespace AmazonApiServer.Repositories
{
	public class ProductRepository(ApplicationContext context) : IProductRepo
	{
		private readonly ApplicationContext _context = context;

		public async Task<Product> CreateAsync(Product product)
		{
			await _context.Products.AddAsync(product);
			if (product.Displays != null)
			{
				foreach (var display in product.Displays)
				{
					display.Product = product;
					await _context.ProductDisplays.AddAsync(display);
				}
			}

			if (product.Details != null)
			{
				foreach (var detail in product.Details)
				{
					detail.Product = product;
					await _context.ProductDetails.AddAsync(detail);
				}
			}

			if (product.Features != null)
			{
				foreach (var feature in product.Features)
				{
					feature.Product = product;
					await _context.ProductFeatures.AddAsync(feature);
				}
			}
			await _context.SaveChangesAsync();

			return product; // todo maybe include foreign keys for debug purposes
		}

		public async Task<Product?> DeleteAsync(Guid productId)
		{
			//Product? product = await _context.Products.FirstOrDefaultAsync(c => c.Id == productId);
			//if (product is not null)
			//{
			//	_context.Products.Remove(product);
			//	await _context.SaveChangesAsync();
			//}
			//return product; // todo maybe include foreign keys for debug purposes
			var product = await _context.Products
				.Include(p => p.Displays)
				.Include(p => p.Details)
				.Include(p => p.Features)
				.FirstOrDefaultAsync(c => c.Id == productId);

			if (product is null)
				return null;

			// Удаляем связанные коллекции
			_context.ProductDisplays.RemoveRange(product.Displays);
			_context.ProductDetails.RemoveRange(product.Details);
			_context.ProductFeatures.RemoveRange(product.Features);

			// Удаляем сам продукт
			_context.Products.Remove(product);

			await _context.SaveChangesAsync();
			return product;
		}

		public async Task<Product?> EditAsync(Product product)
		{
			//Product? existingProduct = await _context.Products.FirstOrDefaultAsync(c => c.Id == product.Id);
			//if (existingProduct is not null)
			//{
			//	existingProduct.Name = product.Name;
			//	existingProduct.Code = product.Code;
			//	existingProduct.CategoryId = product.CategoryId;
			//	existingProduct.Price = product.Price;
			//	existingProduct.Discount = product.Discount;
			//	existingProduct.Number = product.Number;
			//	existingProduct.Displays = product.Displays;
			//	existingProduct.Details = product.Details;
			//	existingProduct.Features = product.Features;
			//	await _context.SaveChangesAsync();
			//}
			//return existingProduct; // todo maybe include foreign keys for debug purposes

			var existingProduct = await _context.Products
				.Include(p => p.Displays)
				.Include(p => p.Details)
				.Include(p => p.Features)
				.FirstOrDefaultAsync(c => c.Id == product.Id);

			if (existingProduct is null)
				return null;

		
			existingProduct.Name = product.Name;
			existingProduct.Code = product.Code;
			existingProduct.CategoryId = product.CategoryId;
			existingProduct.Price = product.Price;
			existingProduct.Discount = product.Discount;
			existingProduct.Number = product.Number;

			_context.ProductDisplays.RemoveRange(existingProduct.Displays);
			_context.ProductDetails.RemoveRange(existingProduct.Details);
			_context.ProductFeatures.RemoveRange(existingProduct.Features);

			if (product.Displays != null)
			{
				foreach (var display in product.Displays)
				{
					display.Product = existingProduct;
					await _context.ProductDisplays.AddAsync(display);
				}
			}

			if (product.Details != null)
			{
				foreach (var detail in product.Details)
				{
					detail.Product = existingProduct;
					await _context.ProductDetails.AddAsync(detail);
				}
			}

			if (product.Features != null)
			{
				foreach (var feature in product.Features)
				{
					feature.Product = existingProduct;
					await _context.ProductFeatures.AddAsync(feature);
				}
			}

			await _context.SaveChangesAsync();

			return existingProduct;
		}

		public async Task<List<Product>> GetAllAsync(ProductsFilter filter)
		{
			IQueryable<Product> query = _context.Products.AsNoTracking();

			if (filter.CategoryId is not null)
			{
				List<Guid> categoryIds = await GetSubCategoryIdsAsync(filter.CategoryId.Value);
				query = query.Where(p => p.CategoryId.HasValue && categoryIds.Contains(p.CategoryId.Value));
			}
			if (filter.OnlyDiscounted)
			{
				query = query.Where(p => p.Discount != 0);
			}
			if (filter.MinPrice is not null)
			{
				query = query.Where(p => p.Price >= filter.MinPrice);
			}
			if (filter.MaxPrice is not null)
			{
				query = query.Where(p => p.Price <= filter.MaxPrice);
			}
			if (filter.Ratings?.Count > 0)
			{
				query = query.Include(e => e.Reviews);
				query = query.Where(p => (p.Reviews != null && p.Reviews.Count > 0) && filter.Ratings.Contains((int)double.Round(p.Reviews.Average(r => r.Stars))));
			}
			if (filter.IncludeReviews)
			{
				query = query.Include(e => e.Reviews);
			}
			if (filter.TrendingDays.HasValue)
			{
				DateTime fromDate = DateTime.UtcNow.AddDays(-filter.TrendingDays.Value);

				query = query
					.Where(p => p.OrderItems.Any(oi => oi.Order.OrderedOn >= fromDate))
					.OrderByDescending(p => p.OrderItems.Count(oi => oi.Order.OrderedOn >= fromDate));
			}

			query = query.Include(e => e.Displays);

			int skip = (filter.Page - 1) * filter.PageSize;
			return await query.Skip(skip).Take(filter.PageSize).ToListAsync();
		}

		public async Task<Product?> GetByIdAsync(Guid id)
		{
			return await _context.Products
				.Include(e => e.Displays)
				.Include(e => e.Details)
					.ThenInclude(e => e.PropertyKey)
				.Include(e => e.Features)
				.Include(e => e.Reviews)
				.FirstOrDefaultAsync(c => c.Id == id);
		}

		private async Task<List<Guid>> GetSubCategoryIdsAsync(Guid parentId)
		{
			List<Category> allCategories = await _context.Categories.AsNoTracking().ToListAsync();
			List<Guid> guids = [];
			GetChildren(parentId, allCategories, guids);
			return guids;
		}

		private static void GetChildren(Guid parentId, List<Category> allCategories, List<Guid> result)
		{
			List<Category> children = allCategories.Where(c => c.ParentId == parentId).ToList();
			result.Add(parentId);
			foreach (Guid childId in children.Select(c => c.Id))
			{
				GetChildren(childId, allCategories, result);
			}
		}
	}
}
