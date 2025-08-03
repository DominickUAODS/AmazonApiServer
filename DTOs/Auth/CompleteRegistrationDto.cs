using System.ComponentModel.DataAnnotations;

namespace AmazonApiServer.DTOs.Auth
{
	public class CompleteRegistrationDto
	{
		[Required]
		public string Email { get; set; } = string.Empty;

		[Required]
		public string FirstName { get; set; } = string.Empty;

		[Required]
		public string LastName { get; set; } = string.Empty;
	}
}
