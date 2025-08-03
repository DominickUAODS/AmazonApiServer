using AmazonApiServer.DTOs.ReviewReview;
using AmazonApiServer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AmazonApiServer.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ReviewReviewController : ControllerBase
	{
		private readonly IReviewReview _service;

		public ReviewReviewController(IReviewReview service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var data = await _service.GetAllAsync();
			return Ok(data);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var entity = await _service.GetByIdAsync(id);
			if (entity == null) return NotFound();
			return Ok(entity);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] ReviewReviewCreateDto dto)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var created = await _service.CreateAsync(dto);
			return CreatedAtAction(nameof(GetById), new { id = created?.Id }, created);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] ReviewReviewUpdateDto dto)
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
