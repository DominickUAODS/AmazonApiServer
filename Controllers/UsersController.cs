using AmazonApiServer.DTOs.User;
using AmazonApiServer.Interfaces;
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
		[Authorize(Roles = "Admin")]
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
				: CreatedAtAction(nameof(GetUserById), new { id = result.id }, result);
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
			return result == null ? NotFound() : Ok(result);
		}

		[HttpPatch("{id}/restore")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> RestoreUser(Guid id)
		{
			var result = await _users.MarkUnDeleteUserAsync(id);
			return result == null ? NotFound() : Ok(result);
		}

		[HttpPatch("{id}/toggle-role")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> ToggleRole(Guid id)
		{
			var result = await _users.ToggleRoleAsync(id);
			return result == null ? NotFound() : Ok(result);
		}
	}
}
