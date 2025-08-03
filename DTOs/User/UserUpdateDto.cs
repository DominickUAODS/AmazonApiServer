using System.ComponentModel.DataAnnotations;

namespace AmazonApiServer.DTOs.User
{
	public class UserUpdateDto
	{
		[Required]
		public Guid id { get; set; }

		[Required]
		[StringLength(100)]
		public string first_name { get; set; } = string.Empty;

		[Required]
		[StringLength(100)]
		public string last_name { get; set; } = string.Empty;

		[Required]
		[EmailAddress]
		public string email { get; set; } = string.Empty;

		public string? password { get; set; }

		[Required]
		public Guid role_id { get; set; }

		public string? profile_photo { get; set; }

		public bool is_active { get; set; }
	}
}
