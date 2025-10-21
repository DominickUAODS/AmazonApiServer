using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AmazonApiServer.DTOs.Auth
{
	public class ResendCodeDto
	{
		[Required]
		[EmailAddress]
		[JsonPropertyName("email")]
		public string Email { get; set; } = string.Empty;

		[Required]
		[JsonPropertyName("purpose")]
		public string Purpose { get; set; } = string.Empty;
	}
}
