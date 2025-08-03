using AmazonApiServer.Models;
using AmazonApiServer.Services;
using AmazonApiServer.DTOs.User;
using AmazonApiServer.DTOs.Review;
using AmazonApiServer.DTOs.ReviewReview;
using AmazonApiServer.DTOs.Order;

namespace AmazonApiServer.Extensions
{
	public static class UserExtensions
	{
		public static UserDto ToDto(this User user)
		{
			return new UserDto
			{
				id = user.Id,
				first_name = user.FirstName,
				last_name = user.LastName,
				email = user.Email,
				profile_photo = user.ProfilePhoto,
				role = user.Role?.Name ?? "",
				is_active = user.IsActive,
				registration_date = user.RegistrationDate,

				orders = user.Orders?.Select(o => new OrderDto { Id = o.Id }).ToList(),
				wishlist = user.Wishlist?.Select(p => new ProductDto { Id = p.Id }).ToList(),
				reviews = user.Reviews?.Select(r => new ReviewDto { Id = r.Id }).ToList(),
				review_reviews = user.ReviewReviews?.Select(r => new ReviewReviewDto { Id = r.Id }).ToList()
			};
		}

		public static User ToUser(this UserCreateDto dto, string hashedPassword)
		{
			return new User
			{
				Id = Guid.NewGuid(),
				FirstName = dto.FirstName,
				LastName = dto.LastName,
				Email = dto.Email,
				PasswordHash = hashedPassword,
				RoleId = dto.RoleId,
				ProfilePhoto = dto.ProfilePhoto ?? "/images/users/default.jpg",
				IsActive = dto.IsActive
				//RegistrationDate = DateOnly.FromDateTime(DateTime.UtcNow)
			};
		}

		public static void UpdateFromDto(this User user, UserUpdateDto dto)
		{
			user.FirstName = dto.first_name;
			user.LastName = dto.last_name;
			user.Email = dto.email;
			user.RoleId = dto.role_id;
			user.IsActive = dto.is_active;

			if (!string.IsNullOrEmpty(dto.profile_photo))
				user.ProfilePhoto = dto.profile_photo;

			if (!string.IsNullOrEmpty(dto.password))
				user.PasswordHash = PasswordHasher.HashPassword(dto.password);
		}
	}
}
