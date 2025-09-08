using AmazonApiServer.DTOs.User;
using AmazonApiServer.Interfaces;
using AmazonApiServer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmazonApiServer.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUser _users;

		public UsersController(IUser users)
		{
			_users = users;
		}

		[HttpGet]
		[Authorize(Roles = "Administrator")]
		public async Task<IActionResult> GetAllUsers()
		{
			var result = await _users.GetAllUsersAsync();
			return Ok(result);
		}

		[HttpGet("{id}")]
		[Authorize]
		public async Task<IActionResult> GetUserById(Guid id)
		{
			var user = await _users.GetUserByIdAsync(id);
			return user == null ? NotFound() : Ok(user);
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> AddUser(UserCreateDto dto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _users.AddUserAsync(dto);
			return result is null
				? StatusCode(500, new { error = "Could not add user" })
				: CreatedAtAction(nameof(GetUserById), new { id = result.Id }, result);
		}

		[HttpPut]
		[Authorize]
		public async Task<IActionResult> UpdateUser(UserUpdateDto dto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var currentUserId = dto.Id;
			var result = await _users.UpdateUserAsync(dto, currentUserId);

			return result == null
				? StatusCode(403, new { error = "Permission denied or user not found" })
				: Ok(result);
		}

		[HttpDelete("{id}")]
		[Authorize]
		public async Task<IActionResult> DeleteUser(Guid id)
		{
			var result = await _users.MarkDeleteUserAsync(id);
			return result == null
				? StatusCode(404, new { error = "User not found" })
				: Ok(result);
		}

		[HttpPatch("{id}/toggle-status")]
		[Authorize(Roles = "Administrator")]
		public async Task<IActionResult> ToggleUserStatus(Guid id)
		{
			var result = await _users.ToggleStatusAsync(id);
			return result == null
				? StatusCode(404, new { error = "User not found" })
				: Ok(result);
		}

		[HttpPatch("{id}/toggle-role")]
		[Authorize(Roles = "Administrator")]
		public async Task<IActionResult> ToggleRole(Guid id)
		{
			var result = await _users.ToggleRoleAsync(id);
			return result == null
				? StatusCode(404, new { error = "User not found" })
				: Ok(result);
		}

		[HttpGet("search")]
		[Authorize(Roles = "Administrator")]
		public async Task<IActionResult> SearchUsers([FromQuery] string query, [FromQuery] string? role)
		{
			var users = await _users.SearchUsersAsync(query, role);
			return Ok(users);
		}

		[HttpPost("{userId}/wishlist/{productId}/toggle")]
		[Authorize]
		public async Task<IActionResult> ToggleFavorite(Guid userId, Guid productId)
		{
			var user = await _users.ToggleFavoriteAsync(userId, productId);
			if (user == null) return NotFound();

			return Ok(user);
		}
	}
}
