using System.ComponentModel.DataAnnotations;

namespace AmazonApiServer.DTOs.Auth
{
	public class EmailCodeVerificationDto
	{
		[Required]
		public string Email { get; set; } = string.Empty;

		[Required]
		[StringLength(6, MinimumLength = 6)]
		public string Code { get; set; } = string.Empty;
	}
}
