using AmazonApiServer.DTOs.Order;
using AmazonApiServer.DTOs.Product;
using AmazonApiServer.DTOs.Review;
using AmazonApiServer.DTOs.ReviewReview;
using AmazonApiServer.DTOs.User;
using AmazonApiServer.Models;
using AmazonApiServer.Services;

namespace AmazonApiServer.Extensions
{
	public static class UserExtensions
	{
		public static UserDto ToDto(this User user)
		{
			return new UserDto
			{
				Id = user.Id,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email,
				ProfilePhoto = user.ProfilePhoto,
				Role = user.Role?.Name ?? "Customer",
				IsActive = user.IsActive,
				RegistrationDate = user.RegistrationDate,

				Orders = user.Orders?.Select(o => new OrderDto { Id = o.Id }).ToList(),
				WishList = user.Wishlist?.Select(p => new ProductDto { Id = p.Id }).ToList(),
				Reviews = user.Reviews?.Select(r => new ReviewDto { Id = r.Id }).ToList(),
				ReviewReviews = user.ReviewReviews?.Select(r => new ReviewReviewDto { Id = r.Id }).ToList()
			};
		}

		public static User ToUser(this UserCreateDto dto, string hashedPassword, Guid roleId)
		{
			return new User
			{
				Id = Guid.NewGuid(),
				FirstName = dto.FirstName,
				LastName = dto.LastName,
				Email = dto.Email,
				PasswordHash = hashedPassword,
				RoleId = roleId,
				ProfilePhoto = "",
				IsActive = dto.IsActive
				//RegistrationDate = DateOnly.FromDateTime(DateTime.UtcNow)
			};
		}

		public static void UpdateFromDto(this User user, UserUpdateDto dto, Guid roleId)
		{
			if (!string.IsNullOrEmpty(dto.FirstName))
				user.FirstName = dto.FirstName;

			if (!string.IsNullOrEmpty(dto.LastName))
				user.LastName = dto.LastName;

			if (!string.IsNullOrEmpty(dto.Email))
				user.Email = dto.Email;

			user.RoleId = roleId;
			user.IsActive = dto.IsActive;

			if (!string.IsNullOrEmpty(dto.ProfilePhoto))
				user.ProfilePhoto = dto.ProfilePhoto;

			if (!string.IsNullOrEmpty(dto.Password))
				user.PasswordHash = PasswordHasher.HashPassword(dto.Password);
		}
	}
}
