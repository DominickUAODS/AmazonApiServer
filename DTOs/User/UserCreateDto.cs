using System.ComponentModel.DataAnnotations;

namespace AmazonApiServer.DTOs.User
{
	public class UserCreateDto
	{
		[Required]
		[StringLength(100)]
		public string FirstName { get; set; } = string.Empty;

		[Required]
		[StringLength(100)]
		public string LastName { get; set; } = string.Empty;

		[Required]
		[EmailAddress]
		public string Email { get; set; } = string.Empty;

		[Required]
		public string Password { get; set; } = string.Empty;

		[Required]
		public Guid RoleId { get; set; }

		public string? ProfilePhoto { get; set; } = "/images/users/default.jpg";

		public bool IsActive { get; set; } = true;
	}
}
