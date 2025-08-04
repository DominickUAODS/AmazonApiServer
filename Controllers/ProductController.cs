using AmazonApiServer.DTOs.Product;
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
        public async Task<IActionResult> CreateAsync([FromForm] AddProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // todo add image displays
            Product product = new()
            {
                Code = productDto.Code,
                CategoryId = productDto.CategoryId,
                Price = productDto.Price,
                Discount = productDto.Discount,
                Number = productDto.Number
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
        public async Task<IActionResult> EditAsync(Guid id, [FromForm] EditProductDto productDto)
        {
            Product product = new()
            {
                Id = id,
                Code = productDto.Code,
                CategoryId = productDto.CategoryId,
                Price = productDto.Price,
                Discount = productDto.Discount,
                Number = productDto.Number
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
            // todo delete old images
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
            // todo delete image
            return Ok(deleted);
        }
    }
}
