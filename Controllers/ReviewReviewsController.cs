using AmazonApiServer.DTOs.ReviewReview;
using AmazonApiServer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmazonApiServer.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ReviewReviewsController : ControllerBase
	{
		private readonly IReviewReview _reviewReview;

		public ReviewReviewsController(IReviewReview reviewReview)
		{
			_reviewReview = reviewReview;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var data = await _reviewReview.GetAllAsync();
			return Ok(data);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var entity = await _reviewReview.GetByIdAsync(id);
			if (entity == null) return NotFound();
			return Ok(entity);
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Create([FromBody] ReviewReviewCreateDto dto)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var created = await _reviewReview.CreateAsync(dto);
			if (created == null) return BadRequest("Record not created");

			return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
		}

		[HttpPut("{id}")]
		[Authorize]
		public async Task<IActionResult> Update(Guid id, [FromBody] ReviewReviewUpdateDto dto)
		{
			if (id != dto.Id) return BadRequest("ID mismatch");
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var updated = await _reviewReview.UpdateAsync(dto);
			if (updated == null) return NotFound();

			return Ok(updated);
		}

		[HttpDelete("{id}")]
		[Authorize]
		public async Task<IActionResult> Delete(Guid id)
		{
			var deleted = await _reviewReview.DeleteAsync(id);
			if (!deleted) return NotFound();

			return NoContent();
		}

		[HttpPost("toggle-helpful")]
		[Authorize]
		public async Task<IActionResult> ToggleHelpful([FromBody] ReviewReviewCreateDto dto)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var result = await _reviewReview.ToggleHelpfulAsync(dto);
			if (result == null) return BadRequest("Recont not updated!");

			return Ok(new { message = "Helpful tag updated" });
		}
	}
}
