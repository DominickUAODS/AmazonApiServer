using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AmazonApiServer.DTOs.Auth
{
	public class EmailCodeVerificationDto
	{
		[Required]
		[JsonPropertyName("email")]
		public string Email { get; set; } = string.Empty;

		[Required]
		[JsonPropertyName("code")]
		[StringLength(6, MinimumLength = 6)]
		public string Code { get; set; } = string.Empty;
	}
}
