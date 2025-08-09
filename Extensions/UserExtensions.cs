using AmazonApiServer.DTOs.Order;
using AmazonApiServer.DTOs.Review;
using AmazonApiServer.DTOs.ReviewReview;
using AmazonApiServer.DTOs.User;
using AmazonApiServer.Interfaces;
using AmazonApiServer.Models;
using AmazonApiServer.Services;

namespace AmazonApiServer.Extensions
{
	public static class UserExtensions
	{
		public static UserDto ToDto(this User user, string? baseUrl = null)
		{
			string profilePhotoUrl;

			if (!string.IsNullOrEmpty(user.ProfilePhoto))
			{
				// Если фото уже содержит полный путь (http/https), оставляем как есть
				if (user.ProfilePhoto.StartsWith("http", StringComparison.OrdinalIgnoreCase))
				{
					profilePhotoUrl = user.ProfilePhoto;
				}
				else
				{
					// Если указан baseUrl — собираем полный путь, иначе просто возвращаем как есть
					profilePhotoUrl = baseUrl != null
						? $"{baseUrl.TrimEnd('/')}/{user.ProfilePhoto.TrimStart('/')}"
						: user.ProfilePhoto;
				}
			}
			else
			{
				profilePhotoUrl = baseUrl != null
					? $"{baseUrl.TrimEnd('/')}/images/users/default.jpg"
					: "/images/users/default.jpg";
			}

			return new UserDto
			{
				id = user.Id,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email,
				ProfilePhoto = profilePhotoUrl,
				Role = user.Role?.Name ?? "Customer",
				IsActive = user.IsActive,
				RegistrationDate = user.RegistrationDate,

				Orders = user.Orders?.Select(o => new OrderDto { Id = o.Id }).ToList(),
				WishList = user.Wishlist?.Select(p => new ProductDto { Id = p.Id }).ToList(),
				Reviews = user.Reviews?.Select(r => new ReviewDto { Id = r.Id }).ToList(),
				ReviewReviews = user.ReviewReviews?.Select(r => new ReviewReviewDto { Id = r.Id }).ToList()
			};
		}

		public static async Task<User> ToUserAsync(this UserCreateDto dto, string hashedPassword, Guid roleId, IImageService imageService)
		{
			string profilePhotoUrl = "/images/users/default.jpg";

			if (dto.ProfilePhoto != null && dto.ProfilePhoto.Length > 0)
			{
				profilePhotoUrl = await imageService.UploadAsync(dto.ProfilePhoto);
			}

			return new User
			{
				Id = Guid.NewGuid(),
				FirstName = dto.FirstName,
				LastName = dto.LastName,
				Email = dto.Email,
				PasswordHash = hashedPassword,
				RoleId = roleId,
				ProfilePhoto = profilePhotoUrl,
				IsActive = dto.IsActive
				//RegistrationDate = DateOnly.FromDateTime(DateTime.UtcNow)
			};
		}

		public static async Task UpdateFromDtoAsync(this User user, UserUpdateDto dto, Guid roleId, IImageService imageService)
		{
			if (!string.IsNullOrEmpty(dto.FirstName))
				user.FirstName = dto.FirstName;

			if (!string.IsNullOrEmpty(dto.LastName))
				user.LastName = dto.LastName;

			if (!string.IsNullOrEmpty(dto.Email))
				user.Email = dto.Email;

			user.RoleId = roleId;
			user.IsActive = dto.IsActive;

			if (dto.ProfilePhoto != null && dto.ProfilePhoto.Length > 0)
			{
				var uploadedUrl = await imageService.UploadAsync(dto.ProfilePhoto);
				user.ProfilePhoto = uploadedUrl;
			}

			if (!string.IsNullOrEmpty(dto.Password))
				user.PasswordHash = PasswordHasher.HashPassword(dto.Password);
		}
	}
}
