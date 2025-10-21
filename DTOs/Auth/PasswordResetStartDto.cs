using System.Text.Json.Serialization;

namespace AmazonApiServer.DTOs.Auth
{
	public class PasswordResetStartDto
	{
		[JsonPropertyName("email")]
		public string Email { get; set; } = null!;
	}
}
