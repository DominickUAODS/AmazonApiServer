using AmazonApiServer.DTOs.Review;
using AmazonApiServer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmazonApiServer.Controllers
{
	namespace AmazonApiServer.Controllers
	{
		[ApiController]
		[Route("api/[controller]")]
		public class ReviewController : ControllerBase
		{
			private readonly IReview _service;

			public ReviewController(IReview service)
			{
				_service = service;
			}

			[HttpGet]
			public async Task<IActionResult> GetAll()
			{
				var reviews = await _service.GetAllAsync();
				return Ok(reviews);
			}

			[HttpGet("{id}")]
			public async Task<IActionResult> GetById(Guid id)
			{
				var review = await _service.GetByIdAsync(id);
				if (review == null) return NotFound();
				return Ok(review);
			}

			[HttpPost]
			public async Task<IActionResult> Create([FromBody] ReviewCreateDto dto)
			{
				if (!ModelState.IsValid) return BadRequest(ModelState);

				var created = await _service.CreateAsync(dto);
				return CreatedAtAction(nameof(GetById), new { id = created?.Id }, created);
			}

			[HttpPut("{id}")]
			public async Task<IActionResult> Update(Guid id, [FromBody] ReviewUpdateDto dto)
			{
				if (id != dto.Id) return BadRequest("ID mismatch");
				if (!ModelState.IsValid) return BadRequest(ModelState);

				var updated = await _service.UpdateAsync(dto);
				if (updated == null) return NotFound();

				return Ok(updated);
			}

			[HttpDelete("{id}")]
			public async Task<IActionResult> Delete(Guid id)
			{
				var deleted = await _service.DeleteAsync(id);
				if (!deleted) return NotFound();

				return NoContent();
			}
		}
	}
}
