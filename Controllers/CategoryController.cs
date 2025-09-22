using Microsoft.AspNetCore.Mvc;
using AmazonApiServer.Interfaces;
using AmazonApiServer.Models;
using AmazonApiServer.DTOs.Category;

namespace AmazonApiServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController(ICategoryRepo categories) : ControllerBase
    {
        private readonly ICategoryRepo _categories = categories;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            List<Category> categoriesList = await _categories.GetAllAsync();
            List<CategoryInListDto> categoryDtosList = categoriesList.Select(c => new CategoryInListDto
            {
                Id = c.Id,
                Name = c.Name,
                Icon = c.Icon,
                Image = c.Image,
                IsActive = c.IsActive,
                Description = c.Description,
                ParentId = c.ParentId,
            }).ToList();
            return Ok(categoryDtosList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            Category? category = await _categories.GetByIdAsync(id);
            if (category is null)
            {
                return NotFound();
            }
            CategoryDto categoryDto = new()
            {
                Name = category.Name,
                Icon = category.Icon,
                Image = category.Image,
                IsActive = category.IsActive,
                Description = category.Description,
                ParentId = category.ParentId,
                PropertyKeys = category.PropertyKeys?.Select(p => p.Name).ToList() ?? []
            };
            return Ok(categoryDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] AddCategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Category category = new()
            {
                Image = categoryDto.Image,
                Icon = categoryDto.Icon,
                Name = categoryDto.Name,
                IsActive = categoryDto.IsActive,
                Description = categoryDto.Description,
                ParentId = categoryDto.ParentId,
                PropertyKeys = categoryDto.PropertyKeys?.Select(p => new PropertyKey { Name = p }).ToList()
            };
            Category created;
            try
            {
                created = await _categories.CreateAsync(category);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAsync(Guid id, [FromForm] EditCategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Category category = new()
            {
                Id = id,
                Image = categoryDto.Image,
                Icon = categoryDto.Icon,
                Name = categoryDto.Name,
                IsActive = categoryDto.IsActive,
                Description = categoryDto.Description,
                ParentId = categoryDto.ParentId,
                PropertyKeys = categoryDto.PropertyKeys?.Select(p => new PropertyKey { Name = p }).ToList()
            };
            Category? result;
            try
            {
                result = await _categories.EditAsync(category);
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
            return Ok(deleted);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchCategories([FromQuery] string query)
        {
			List<Category>? categoriesList = await _categories.SearchCategoriesAsync(query);

            if (categoriesList != null)
            {
                List<CategoryInListDto> categoryDtosList = categoriesList.Select(c => new CategoryInListDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Icon = c.Icon,
                    Image = c.Image,
                    IsActive = c.IsActive,
                    Description = c.Description,
                    ParentId = c.ParentId,
                }).ToList();
                return Ok(categoryDtosList);
            }
            return NotFound();
		}

		[HttpGet("{categoryId}/breadcrumbs")]
		public async Task<IActionResult> GetBreadcrumbs(Guid categoryId)
		{
			var crumbs = await _categories.GetBreadcrumbsAsync(categoryId);
			return Ok(crumbs);
		}
	}
}
