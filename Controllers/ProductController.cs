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
    public class ProductController(IProductRepo products, IImageService imageService) : ControllerBase
    {
        private readonly IProductRepo _products = products;
        private readonly IImageService _imageService = imageService;

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
                Display = p.Displays?.Select(d => d.Image).First() ?? string.Empty,
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
                Price = product.Price,
                Discount = product.Discount,
                Number = product.Number,
                Displays = product.Displays?.Select(d => d.Image).ToList() ?? [],
                Details = product.Details?.Select(d => new ProductDetailDto { PropertyKey = d.PropertyKey, Attribute = d.Attribute }).ToList() ?? [],
                Features = product.Features?.Select(f => new ProductFeatureDto { Name = f.Name, Description = f.Description }).ToList() ?? []
            };
            return Ok(productDto);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateAsync([FromForm] AddProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IEnumerable<Task<string>> uploadTasks = productDto.Displays.Select(_imageService.UploadAsync);
            string[] displayPaths = await Task.WhenAll(uploadTasks);
            Product product = new()
            {
                Name = productDto.Name,
                Code = productDto.Code,
                CategoryId = productDto.CategoryId,
                Price = productDto.Price,
                Discount = productDto.Discount,
                Number = productDto.Number,
                Displays = displayPaths.Select(p => new ProductDisplay { Image = p }).ToList(),
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
                IEnumerable<Task> deleteTasks = displayPaths.Select(_imageService.DeleteAsync);
                await Task.WhenAll(deleteTasks);
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
            IEnumerable<Task<string>> uploadTasks = productDto.Displays.Select(_imageService.UploadAsync);
            string[] displayPaths = await Task.WhenAll(uploadTasks);
            Product product = new()
            {
                Id = id,
                Name = productDto.Name,
                Code = productDto.Code,
                CategoryId = productDto.CategoryId,
                Price = productDto.Price,
                Discount = productDto.Discount,
                Number = productDto.Number,
                Displays = displayPaths.Select(p => new ProductDisplay { Image = p }).ToList(),
                Details = productDto.ProductDetails?.Select(d => new ProductDetail { PropertyKey = d.PropertyKey, Attribute = d.Attribute }).ToList(),
                Features = productDto.ProductFeatures?.Select(f => new ProductFeature { Name = f.Name, Description = f.Description }).ToList()
            };
            Product? result;
            IEnumerable<string>? oldDisplays = (await _products.GetByIdAsync(id))?.Displays?.Select(d => d.Image);
            try
            {
                result = await _products.EditAsync(product);
            }
            catch (Exception ex)
            {
                IEnumerable<Task> deleteTasks = displayPaths.Select(_imageService.DeleteAsync);
                await Task.WhenAll(deleteTasks);
                return Problem(ex.Message);
            }
            if (result is null)
            {
                return NotFound();
            }
            IEnumerable<Task> deleteOldTasks = oldDisplays?.Select(_imageService.DeleteAsync) ?? [];
            await Task.WhenAll(deleteOldTasks);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            Product? deleted;
            IEnumerable<string>? oldDisplays = (await _products.GetByIdAsync(id))?.Displays?.Select(d => d.Image);
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
            IEnumerable<Task> deleteOldTasks = oldDisplays?.Select(_imageService.DeleteAsync) ?? [];
            await Task.WhenAll(deleteOldTasks);
            return Ok(deleted);
        }
    }
}
