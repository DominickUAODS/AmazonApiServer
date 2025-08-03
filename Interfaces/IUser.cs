using AmazonApiServer.DTOs.User;
using AmazonApiServer.Models;

namespace AmazonApiServer.Interfaces
{
	public interface IUser
	{
		Task<IEnumerable<UserDto>> GetAllUsersAsync();
		Task<UserDto?> GetUserByIdAsync(Guid id);
		Task<UserDto?> AddUserAsync(UserCreateDto dto);
		Task<UserDto?> UpdateUserAsync(UserUpdateDto dto, Guid currentUserId);
		Task<UserDto?> MarkDeleteUserAsync(Guid id);
		Task<UserDto?> MarkUnDeleteUserAsync(Guid id);
		Task<UserDto?> ToggleRoleAsync(Guid id);
	}
}
