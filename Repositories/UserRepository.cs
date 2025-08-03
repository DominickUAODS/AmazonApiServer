using AmazonApiServer.Data;
using AmazonApiServer.DTOs.User;
using AmazonApiServer.Extensions;
using AmazonApiServer.Interfaces;
using AmazonApiServer.Models;
using AmazonApiServer.Services;
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

		public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
		{
			var users = await _context.Users
				.Include(u => u.Role)
				.Include(u => u.Orders)
				.Include(u => u.Wishlist)
				.Include(u => u.Reviews)
				.Include(u => u.ReviewReviews)
				.ToListAsync();

			return users.Select(u => u.ToDto());
		}

		public async Task<UserDto?> GetUserByIdAsync(Guid id)
		{
			var user = await _context.Users
				.Include(u => u.Role)
				.Include(u => u.Orders)
				.Include(u => u.Wishlist)
				.Include(u => u.Reviews)
				.Include(u => u.ReviewReviews)
				.FirstOrDefaultAsync(u => u.Id == id);

			return user?.ToDto();
		}

		public async Task<UserDto?> AddUserAsync(UserCreateDto dto)
		{
			var role = await _context.Roles.FindAsync(dto.RoleId);
			if (role == null) return null;

			var hashedPassword = PasswordHasher.HashPassword(dto.Password);
			var user = dto.ToUser(hashedPassword);

			_context.Users.Add(user);
			await _context.SaveChangesAsync();

			return user.ToDto();
		}

		public async Task<UserDto?> UpdateUserAsync(UserUpdateDto dto, Guid currentUserId)
		{
			if (dto.id != currentUserId) return null;

			var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == dto.id);
			if (user == null) return null;

			user.UpdateFromDto(dto);
			await _context.SaveChangesAsync();

			return user.ToDto();
		}

		public async Task<UserDto?> MarkDeleteUserAsync(Guid id)
		{
			var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);
			if (user == null) return null;

			user.IsActive = false;
			await _context.SaveChangesAsync();

			return user.ToDto();
		}

		public async Task<UserDto?> MarkUnDeleteUserAsync(Guid id)
		{
			var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);
			if (user == null) return null;

			user.IsActive = true;
			await _context.SaveChangesAsync();

			return user.ToDto();
		}

		public async Task<UserDto?> ToggleRoleAsync(Guid id)
		{
			var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);
			if (user == null) return null;

			var admin = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");
			var customer = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Customer");
			if (admin == null || customer == null) return null;

			user.RoleId = user.Role?.Name == "Admin" ? customer.Id : admin.Id;
			await _context.SaveChangesAsync();

			return user.ToDto();
		}
	}
}
