using System.Text.Json.Serialization;

namespace AmazonApiServer.DTOs.Auth
{
	public class AuthResponseDto
	{
		[JsonPropertyName("access_token")]
		public string AccessToken { get; set; } = string.Empty;

		[JsonPropertyName("refresh_token")]
		public string RefreshToken { get; set; } = string.Empty;

		[JsonPropertyName("user")]
		public object User { get; set; } = new();
	}
}
