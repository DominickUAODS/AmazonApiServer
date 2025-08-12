using AmazonApiServer.DTOs.Product;
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
        public async Task<IActionResult> GetAllAsync()
        {
            List<Product> productsList = await _products.GetAllAsync();
            return Ok(productsList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            Product? product = await _products.GetByIdAsync(id);
            return product is null ? NotFound() : Ok(product);
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
