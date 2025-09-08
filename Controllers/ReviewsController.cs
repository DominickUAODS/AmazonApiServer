using AmazonApiServer.DTOs.Review;
using AmazonApiServer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmazonApiServer.Controllers
{
	namespace AmazonApiServer.Controllers
	{
		[ApiController]
		[Route("api/[controller]")]
		public class ReviewsController : ControllerBase
		{
			private readonly IReview _review;

			public ReviewsController(IReview review)
			{
				_review = review;
			}

			[HttpGet]
			public async Task<IActionResult> GetAll()
			{
				var reviews = await _review.GetAllAsync();
				return Ok(reviews);
			}

			[HttpGet("{id}")]
			public async Task<IActionResult> GetById(Guid id) {

				var review = await _review.GetByIdAsync(id);
				return review == null ? NotFound() : Ok(review);
			}

			[HttpGet("by-user/{userId}")]
			public async Task<IActionResult> GetByUserId(Guid userId)
			{
				var reviews = await _review.GetByUserIdAsync(userId);
				return reviews == null ? NotFound() : Ok(reviews);
			}

			[HttpGet("by-product/{productId}")]
			public async Task<IActionResult> GetByProductId(Guid productId)
			{
				var reviews = await _review.GetByProductIdAsync(productId);
				return reviews == null ? NotFound() : Ok(reviews);
			}

			[HttpPost]
			[Authorize]
			public async Task<IActionResult> Create([FromBody] ReviewCreateDto dto)
			{
				if (!ModelState.IsValid) return BadRequest(ModelState);

				var created = await _review.CreateAsync(dto);
				return CreatedAtAction(nameof(GetById), new { id = created?.Id }, created);
			}

			[HttpPut("{id}")]
			[Authorize]
			public async Task<IActionResult> Update(Guid id, [FromBody] ReviewUpdateDto dto)
			{
				if (!ModelState.IsValid) return BadRequest(ModelState);

				if (id != dto.Id) return BadRequest("ID mismatch");
				if (!ModelState.IsValid) return BadRequest(ModelState);

				var updated = await _review.UpdateAsync(dto);
				if (updated == null) return NotFound();

				return Ok(updated);
			}

			[HttpDelete("{id}")]
			[Authorize]
			public async Task<IActionResult> Delete(Guid id)
			{
				var deleted = await _review.DeleteAsync(id);
				if (!deleted) return NotFound();

				return NoContent();
			}
		}
	}
}
