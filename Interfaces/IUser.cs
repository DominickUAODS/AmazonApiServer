using AmazonApiServer.DTOs.User;

namespace AmazonApiServer.Interfaces
{
	public interface IUser
	{
		Task<IEnumerable<UserDto>> GetAllUsersAsync();
		Task<UserDto?> GetUserByIdAsync(Guid id);
		Task<UserDto?> AddUserAsync(UserCreateDto dto);
		Task<UserDto?> UpdateUserAsync(UserUpdateDto dto, Guid currentUserId);
		Task<UserDto?> MarkDeleteUserAsync(Guid id);
		Task<UserDto?> ToggleStatusAsync(Guid id);
		Task<UserDto?> ToggleRoleAsync(Guid id);
		Task<UserDto?> ToggleFavoriteAsync(Guid userId, Guid productId);
		Task<IEnumerable<UserDto>> SearchUsersAsync(string query, string? role);
	}
}
