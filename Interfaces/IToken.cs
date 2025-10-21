using AmazonApiServer.Models;

namespace AmazonApiServer.Interfaces
{
	public interface IToken
	{
		string CreateJwtToken(User user);
		Task<string> CreateRefreshTokenAsync(User user);
	}
}
