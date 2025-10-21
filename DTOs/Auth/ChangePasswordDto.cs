using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AmazonApiServer.DTOs.Auth
{
	public class ChangePasswordDto
	{
		[Required]
		[JsonPropertyName("current_password")]
		public string CurrentPassword { get; set; } = string.Empty;

		[Required]
		[JsonPropertyName("new_password")]
		public string NewPassword { get; set; } = string.Empty;
	}
}
