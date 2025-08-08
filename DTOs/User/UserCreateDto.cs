using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AmazonApiServer.DTOs.User
{
	public class UserCreateDto
	{
		[Required]
		[StringLength(100)]
		[JsonPropertyName("first_name")]
		public string FirstName { get; set; } = string.Empty;

		[Required]
		[StringLength(100)]
		[JsonPropertyName("last_name")]
		public string LastName { get; set; } = string.Empty;

		[Required]
		[EmailAddress]
		[JsonPropertyName("email")]
		public string Email { get; set; } = string.Empty;

		[Required]
		[JsonPropertyName("password")]
		public string Password { get; set; } = string.Empty;

		[Required]
		[JsonPropertyName("role")]
		public string Role { get; set; } = string.Empty;

		[JsonPropertyName("profile_photo")]
		public string? ProfilePhoto { get; set; } = "/images/users/default.jpg";

		[JsonPropertyName("is_active")]
		public bool IsActive { get; set; } = true;
	}
}
