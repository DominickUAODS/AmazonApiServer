using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AmazonApiServer.DTOs.Order;
using AmazonApiServer.DTOs.Review;
using AmazonApiServer.DTOs.ReviewReview;

namespace AmazonApiServer.DTOs.User
{
	public class UserDto
	{
		[Required]
		public Guid id { get; set; }

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
		[JsonPropertyName("role")]
		public string Role { get; set; } = string.Empty;

		[Required]
		[Display(Name = "Is Active")]
		public bool IsActive { get; set; } = true;

		[JsonPropertyName("profile_photo")]
		[Url(ErrorMessage = "Invalid URL format")]
		public string ProfilePhoto { get; set; } = string.Empty;

		[Display(Name = "Registration Date")]
		[DataType(DataType.Date)]
		public DateTime RegistrationDate { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public List<OrderDto>? Orders { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public List<ProductDto>? WishList { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public List<ReviewDto>? Reviews { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public List<ReviewReviewDto>? ReviewReviews { get; set; }
	}

	public class ProductDto { public Guid Id { get; set; } public string Name { get; set; } = string.Empty; }
}


