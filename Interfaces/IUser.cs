using AmazonApiServer.Models;

namespace AmazonApiServer.Interfaces
{
	public interface IUser
	{
		Task<IEnumerable<User>> GetAllUsersAsync();
		Task<User?> GetUserByIdAsync(Guid id);
		Task<bool> MarkDeleteUserAsync(Guid id);
		Task<bool> MarkUnDeleteUserAsync(Guid id);
		Task<bool> AddUserAsync(User user);
		Task<bool> UpdateUserAsync(User user, Guid currentUserId);
	}
}
