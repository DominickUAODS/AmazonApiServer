using AmazonApiServer.Data;
using AmazonApiServer.Interfaces;
using AmazonApiServer.Models;
using Microsoft.EntityFrameworkCore;

namespace AmazonApiServer.Repositories
{
	public class UserRepository : IUser
	{
		private readonly ApplicationContext _context;

		public UserRepository(ApplicationContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<User>> GetAllUsersAsync()
		{
			return await _context.Users.ToListAsync();
		}

		public async Task<User?> GetUserByIdAsync(Guid id)
		{
			return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
		}

		public async Task<bool> MarkDeleteUserAsync(Guid id)
		{
			var user = await GetUserByIdAsync(id);
			if (user == null) return false;

			user.IsActive = false;
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> MarkUnDeleteUserAsync(Guid id)
		{
			var user = await GetUserByIdAsync(id);
			if (user == null) return false;

			user.IsActive = true;
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> AddUserAsync(User user)
		{
			_context.Users.Add(user);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> UpdateUserAsync(User user, Guid currentUserId)
		{
			if (user.Id != currentUserId) return false;

			var currentUser = await GetUserByIdAsync(user.Id);
			if (currentUser == null) return false;

			currentUser.FirstName = user.FirstName;
			currentUser.LastName = user.LastName;
			currentUser.Email = user.Email;
			currentUser.ProfilePhoto = user.ProfilePhoto;
			currentUser.Email = user.Email;

			await _context.SaveChangesAsync();
			return true;
		}
	}
}
