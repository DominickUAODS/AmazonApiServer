using System.Text.Json.Serialization;

namespace AmazonApiServer.DTOs.Auth
{
	public class PasswordResetCompleteDto
	{
		[JsonPropertyName("email")]
		public string Email { get; set; } = null!;

		[JsonPropertyName("code")]
		public string Code { get; set; } = null!;
		
		[JsonPropertyName("new_password")]
		public string NewPassword { get; set; } = null!;
	}
}
