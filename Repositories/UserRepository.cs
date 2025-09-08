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
			var role = await _context.Roles.FirstOrDefaultAsync(u => u.Name == dto.Role);
			if (role == null) return null;

			var hashedPassword = PasswordHasher.HashPassword(dto.Password);
			var user = dto.ToUser(hashedPassword, role.Id);

			_context.Users.Add(user);
			await _context.SaveChangesAsync();

			return user.ToDto();
		}

		public async Task<UserDto?> UpdateUserAsync(UserUpdateDto dto, Guid currentUserId)
		{
			if (dto.Id != currentUserId) return null;

			var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == dto.Id);
			if (user == null) return null;

			var role = await _context.Roles.FirstOrDefaultAsync(u => u.Name == dto.Role);
			if (role == null) return null;

			user.UpdateFromDto(dto, role.Id);

			// при смене ЛЮБЫХ данных блочим и отзываем токены
			// нужен сервис по удалению просоченных или отозванных токенов
			// можно заглушить на время тестов
			var tokens = await _context.RefreshTokens.Where(t => t.UserId == user.Id && !t.IsRevoked).ToListAsync();
			foreach (var t in tokens)
				t.IsRevoked = true;

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

		public async Task<UserDto?> ToggleStatusAsync(Guid id)
		{
			var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);
			if (user == null) return null;

			user.IsActive = !user.IsActive;

			await _context.SaveChangesAsync();

			return user.ToDto();
		}

		public async Task<UserDto?> ToggleRoleAsync(Guid id)
		{
			var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);
			if (user == null) return null;

			var admin = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Administrator");
			var customer = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Customer");
			if (admin == null || customer == null) return null;

			user.RoleId = user.Role?.Name == "Administrator" ? customer.Id : admin.Id;
			await _context.SaveChangesAsync();

			return user.ToDto();
		}

		public async Task<IEnumerable<UserDto>> SearchUsersAsync(string query, string? roleName)
		{
			var usersQuery = _context.Users.Include(u => u.Role).AsQueryable();

			if (!string.IsNullOrWhiteSpace(query))
			{
				var q = query.ToLower();
				usersQuery = usersQuery.Where(u =>
					u.FirstName.ToLower().Contains(q) ||
					u.LastName.ToLower().Contains(q) ||
					u.Email.ToLower().Contains(q)
				);
			}

			if (!string.IsNullOrWhiteSpace(roleName))
			{
				usersQuery = usersQuery.Where(u => u.Role != null && u.Role.Name == roleName);
			}

			var users = await usersQuery.OrderBy(u => u.FirstName).ToListAsync();

			return users.Select(u => u.ToDto());
		}

		public async Task<UserDto?> ToggleFavoriteAsync(Guid userId, Guid productId)
		{
			var user = await _context.Users
				.Include(u => u.Wishlist)
				.FirstOrDefaultAsync(u => u.Id == userId);

			if (user == null) return null;

			var product = await _context.Products.FindAsync(productId);
			if (product == null) return null;

			if (user.Wishlist.Any(p => p.Id == productId))
			{
				user.Wishlist.Remove(product);
			}
			else
			{
				user.Wishlist.Add(product);
			}

			await _context.SaveChangesAsync();

			return new UserDto
			{
				Id = user.Id,
				Email = user.Email,
				WishList = user.Wishlist.Select(p => new ProductDto
				{
					Id = p.Id,
					Name = p.Name,
				}).ToList()
			};
		}

	}
}
