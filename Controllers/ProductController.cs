using AmazonApiServer.DTOs.Category;
using AmazonApiServer.DTOs.Product;
using AmazonApiServer.DTOs.ProductDetail;
using AmazonApiServer.DTOs.ProductFeature;
using AmazonApiServer.Filters;
using AmazonApiServer.Interfaces;
using AmazonApiServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace AmazonApiServer.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductController(IProductRepo products) : ControllerBase
	{
		private readonly IProductRepo _products = products;

		[HttpGet]
		public async Task<IActionResult> GetAllAsync([FromQuery] ProductsFilter filter)
		{
			List<Product> productsList = await _products.GetAllAsync(filter);
			IEnumerable<ProductInListDto> productDtosList = productsList.Select(p => new ProductInListDto
			{
				Id = p.Id,
				Name = p.Name,
				Price = p.Price,
				Discount = p.Discount,
				Display = p.Displays?.Select(d => d.Image).FirstOrDefault() ?? string.Empty,
				Rating = filter.IncludeReviews ? ((p.Reviews != null && p.Reviews.Count > 0) ? p.Reviews.Average(r => r.Stars) : 0) : null,
				Comments = filter.IncludeReviews ? p.Reviews?.Count : null
			});
			return Ok(productDtosList);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetByIdAsync(Guid id)
		{
			Product? product = await _products.GetByIdAsync(id);
			if (product is null)
			{
				return NotFound();
			}
			ProductDto productDto = new()
			{
				Name = product.Name,
				Code = product.Code,
				CategoryId = product.CategoryId,
				Category = product.Category == null ? null : new CategoryDto { Id = product.Category.Id, Name = product.Category.Name, ParentId = product.Category.ParentId },
				Price = product.Price,
				Discount = product.Discount,
				Number = product.Number,
				Displays = product.Displays?.Select(d => d.Image).ToList() ?? [],
				Details = product.Details?.OrderByDescending(d => d.Id).Select(d => new ProductDetailDto { PropertyKey = d.PropertyKey, Attribute = d.Attribute }).ToList() ?? [],
				Features = product.Features?.OrderByDescending(f => f.Id).Select(f => new ProductFeatureDto { Name = f.Name, Description = f.Description }).ToList() ?? [],
				Stars = (int)Math.Floor(product.Reviews?.Select(r => r.Stars).DefaultIfEmpty(0).Average() ?? 0),
				Comments = product.Reviews?.Count ?? 0
			};
			return Ok(productDto);
		}

		[HttpPost]
		public async Task<IActionResult> CreateAsync([FromForm] AddProductDto productDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			Product product = new()
			{
				Name = productDto.Name,
				Code = productDto.Code,
				CategoryId = productDto.CategoryId,
				Price = productDto.Price,
				Discount = productDto.Discount,
				Number = productDto.Number,
				Displays = productDto.Displays?.Select(p => new ProductDisplay { Image = p }).ToList() ?? [],
				Details = productDto.ProductDetails?.Select(d => new ProductDetail { PropertyKey = d.PropertyKey, Attribute = d.Attribute }).ToList(),
				Features = productDto.ProductFeatures?.Select(f => new ProductFeature { Name = f.Name, Description = f.Description }).ToList()
			};
			Product created;
			try
			{
				created = await _products.CreateAsync(product);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
			return Ok(created);
		}

		[HttpPut("{id}")]
		[Consumes("multipart/form-data")]
		public async Task<IActionResult> EditAsync(Guid id, [FromForm] EditProductDto productDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			Product product = new()
			{
				Id = id,
				Name = productDto.Name,
				Code = productDto.Code,
				CategoryId = productDto.CategoryId,
				Price = productDto.Price,
				Discount = productDto.Discount,
				Number = productDto.Number,
				Displays = productDto.Displays?.Select(p => new ProductDisplay { Image = p }).ToList() ?? [],
				Details = productDto.ProductDetails?.Select(d => new ProductDetail { PropertyKey = d.PropertyKey, Attribute = d.Attribute }).ToList(),
				Features = productDto.ProductFeatures?.Select(f => new ProductFeature { Name = f.Name, Description = f.Description }).ToList()
			};
			Product? result;
			try
			{
				result = await _products.EditAsync(product);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
			if (result is null)
			{
				return NotFound();
			}
			return Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAsync(Guid id)
		{
			Product? deleted;
			try
			{
				deleted = await _products.DeleteAsync(id);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
			if (deleted is null)
			{
				return NotFound();
			}
			return Ok(deleted);
		}
	}
}
