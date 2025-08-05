using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AmazonApiServer.DTOs.Auth
{
	public class CompleteRegistrationDto
	{
		[Required]
		[JsonPropertyName("email")]
		public string Email { get; set; } = string.Empty;

		[Required]
		[JsonPropertyName("first_name")]
		public string FirstName { get; set; } = string.Empty;

		[Required]
		[JsonPropertyName("last_name")]
		public string LastName { get; set; } = string.Empty;
	}
}
