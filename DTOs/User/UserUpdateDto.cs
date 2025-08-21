using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AmazonApiServer.DTOs.User
{
	public class UserUpdateDto
	{
		[Required]
		public Guid Id { get; set; }

		//[Required]
		[StringLength(100)]
		[JsonPropertyName("first_name")]
		public string? FirstName { get; set; }

		//[Required]
		[StringLength(100)]
		[JsonPropertyName("last_name")]
		public string? LastName { get; set; }

		//[Required]
		[EmailAddress]
		[JsonPropertyName("email")]
		public string? Email { get; set; }

		[JsonPropertyName("password")]
		public string? Password { get; set; }

		//[Required]
		[JsonPropertyName("role")]
		public string? Role { get; set; }

		[JsonPropertyName("profile_photo")]
		public string? ProfilePhoto { get; set; }

		[JsonPropertyName("is_active")]
		public bool IsActive { get; set; } = true;
	}
}
