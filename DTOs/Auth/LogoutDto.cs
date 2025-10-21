using System.Text.Json.Serialization;

namespace AmazonApiServer.DTOs.Auth
{
	public class LogoutDto
	{
		[JsonPropertyName("refresh_token")]
		public string RefreshToken { get; set; } = string.Empty;
	}
}
