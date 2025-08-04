using Microsoft.AspNetCore.Mvc;
using AmazonApiServer.Interfaces;
using AmazonApiServer.Models;
using AmazonApiServer.DTOs.Category;

namespace AmazonApiServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController(ICategoryRepo categories, IImageService imageService) : ControllerBase
    {
        private readonly ICategoryRepo _categories = categories;
        private readonly IImageService _imageService = imageService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            List<Category> categoriesList = await _categories.GetAllAsync();
            return Ok(categoriesList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            Category? category = await _categories.GetByIdAsync(id);
            return category is null ? NotFound() : Ok(category);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateAsync([FromForm] AddCategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Category category = new()
            {
                Image = await _imageService.UploadAsync(categoryDto.Image),
                Icon = categoryDto.Icon,
                Name = categoryDto.Name,
                IsActive = categoryDto.IsActive,
                Description = categoryDto.Description,
                ParentId = categoryDto.ParentId
            };
            Category created;
            try
            {
                created = await _categories.CreateAsync(category);
            }
            catch (Exception ex)
            {
                await _imageService.DeleteAsync(category.Image);
                return Problem(ex.Message);
            }
            return Ok(created);
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> EditAsync(Guid id, [FromForm] EditCategoryDto categoryDto)
        {
            Category category = new()
            {
                Id = id,
                Image = await _imageService.UploadAsync(categoryDto.Image),
                Icon = categoryDto.Icon,
                Name = categoryDto.Name,
                IsActive = categoryDto.IsActive,
                Description = categoryDto.Description,
                ParentId = categoryDto.ParentId
            };
            Category? result;
            string? oldPicture = (await _categories.GetByIdAsync(id))?.Image;
            try
            {
                result = await _categories.EditAsync(category);
            }
            catch (Exception ex)
            {
                await _imageService.DeleteAsync(category.Image);
                return Problem(ex.Message);
            }
            if (result is null)
            {
                return NotFound();
            }
            await _imageService.DeleteAsync(oldPicture!);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            Category? deleted;
            try
            {
                deleted = await _categories.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            if (deleted is null)
            {
                return NotFound();
            }
            await _imageService.DeleteAsync(deleted.Image);
            return Ok(deleted);
        }
    }
}
