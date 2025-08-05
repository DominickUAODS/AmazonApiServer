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
		[Display(Name = "First Name")]
		public string first_name { get; set; } = string.Empty;

		[Required]
		[StringLength(100)]
		[Display(Name = "First Name")]
		public string last_name { get; set; } = string.Empty;

		[Required]
		[EmailAddress]
		public string email { get; set; } = string.Empty;

		[Required]
		[Display(Name = "Role Name")]
		public string role { get; set; } = string.Empty;

		[Required]
		[Display(Name = "Is Active")]
		public bool is_active { get; set; }

		[Display(Name = "Profile Photo URL")]
		[Url(ErrorMessage = "Invalid URL format")]
		public string profile_photo { get; set; } = string.Empty;

		[Display(Name = "Registration Date")]
		[DataType(DataType.Date)]
		public DateTime registration_date { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public List<OrderDto>? orders { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public List<ProductDto>? wishlist { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public List<ReviewDto>? reviews { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public List<ReviewReviewDto>? review_reviews { get; set; }
	}

	public class ProductDto { public Guid Id { get; set; } public string Name { get; set; } = string.Empty; }
}


