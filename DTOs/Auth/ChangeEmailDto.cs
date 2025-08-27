using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AmazonApiServer.DTOs.Auth
{
	public class ChangeEmailDto
	{
		[Required]
		[JsonPropertyName("new_email")]
		public string NewEmail { get; set; } = string.Empty;
	}
}
