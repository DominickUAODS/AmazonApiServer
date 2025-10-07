using AmazonApiServer.Data;
using AmazonApiServer.DTOs.User;
using AmazonApiServer.Extensions;
using AmazonApiServer.Filters;
using AmazonApiServer.Interfaces;
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

		public async Task<IEnumerable<UserDto>> GetAllUsersAsync(UsersFilter filter)
		{
			// Базовый запрос с необходимыми Include
			var query = _context.Users
				.Include(u => u.Role)
				.Include(u => u.Orders)
				.Include(u => u.Wishlist)
				.Include(u => u.Reviews)
				.Include(u => u.ReviewReviews)
				.AsQueryable();

			// Фильтр по ID пользователя (если задан)
			if (filter.UserId.HasValue)
			{
				query = query.Where(u => u.Id == filter.UserId.Value);
			}

			// Фильтр по поиску (имя, фамилия, email)
			if (!string.IsNullOrWhiteSpace(filter.Search))
			{
				var searchLower = filter.Search.ToLower();
				query = query.Where(u =>
					u.FirstName.ToLower().Contains(searchLower) ||
					u.LastName.ToLower().Contains(searchLower) ||
					u.Email.ToLower().Contains(searchLower)
				);
			}

			// Фильтр по роли
			if (!string.IsNullOrWhiteSpace(filter.Role))
			{
				query = query.Where(u => u.Role != null && u.Role.Name == filter.Role);
			}

			// Сортировка
			query = query.OrderBy(u => u.FirstName);

			// Пагинация, пока отключил нужно менять логику выдачи json на вид что + какая страница + общее количество страниц
			// int skip = (filter.Page - 1) * filter.PageSize;
			// query = query.Skip(skip).Take(filter.PageSize);

			var users = await query.ToListAsync();

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

		public async Task<IEnumerable<UserDto>> SearchUsersAsync(string? query, string? roleName)
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

			return user.ToDto();
		}

		public async Task<IEnumerable<UserWishlistDto>> GetWishlistAsync(Guid userId)
		{
			var user = await _context.Users
				.Include(u => u.Wishlist)
				.ThenInclude(p => p.Displays)
				.Include(u => u.Wishlist)
				.ThenInclude(p => p.Reviews)
				.FirstOrDefaultAsync(u => u.Id == userId);

			//var user = await _context.Users.Include(u => u.Wishlist).FirstOrDefaultAsync(u => u.Id == userId);
				

			if (user == null) return [];

			return user.Wishlist.Select(p =>
			{				
				return new UserWishlistDto
				{
					Id = p.Id,
					Name = p.Name,
					Image = p.Displays.FirstOrDefault()?.Image ?? string.Empty,
					Reviews = p.Reviews.Count,
					Rating = p.Reviews.Any() ? (int)p.Reviews.Average(r => r.Stars) : 0,
					Price = (decimal)p.Price,
					Discount = p.Discount,
					OldPrice = Math.Round((decimal)(p.Price * (100 + p.Discount) / 100), 2)
				};
			});
		}
	}
}
