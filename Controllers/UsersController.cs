using AmazonApiServer.Interfaces;
using AmazonApiServer.Models;
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
		//[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetAllUsers()
		{
			var users = await _users.GetAllUsersAsync();
			return Ok(users);
		}

		[HttpGet("{id}")]
		//[Authorize]
		public async Task<IActionResult> GetUserById(Guid id)
		{
			var user = await _users.GetUserByIdAsync(id);
			return user == null ? NotFound() : Ok(user);
		}

		[HttpPost]
		//[AllowAnonymous]
		public async Task<IActionResult> AddUser(User user)
		{
			if (user == null || !ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _users.AddUserAsync(user);
			return result ? Ok(user) : StatusCode(500, new { error = "Could not add user" });
		}

		[HttpPut]
		//[Authorize]
		public async Task<IActionResult> UpdateUser(User user)
		{
			if (user == null || !ModelState.IsValid)
				return BadRequest(ModelState);

			var currentUserId = user.Id;
			var success = await _users.UpdateUserAsync(user, currentUserId);

			return success ? Ok(user) : StatusCode(500, new { error = "Unexpected server error occurred" });
		}

		[HttpDelete("{id}")]
		//[Authorize]
		public async Task<IActionResult> DeleteUser(Guid id)
		{
			var currentUser = await _users.GetUserByIdAsync(id);
			if (currentUser == null)
				return NotFound();

			var success = await _users.MarkDeleteUserAsync(id);
			return success ? Ok() : StatusCode(500, new { error = "Unexpected server error occurred" });
		}

		[HttpPatch("{id}")]
		//[Authorize]
		public async Task<IActionResult> RestoreUser(Guid id)
		{
			var currentUser = await _users.GetUserByIdAsync(id);
			if (currentUser == null)
				return NotFound();

			var success = await _users.MarkDeleteUserAsync(id);
			return success ? Ok() : StatusCode(500, new { error = "Unexpected server error occurred" });
		}
	}
}
