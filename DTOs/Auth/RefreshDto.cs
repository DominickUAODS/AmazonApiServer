using System.Text.Json.Serialization;

namespace AmazonApiServer.DTOs.Auth
{
	public class RefreshDto
	{
		[JsonPropertyName("refresh_token")]
		public string RefreshToken { get; set; } = string.Empty;
	}
}
