using System.Text.Json.Serialization;

namespace AmazonApiServer.Models
{
	public class User
	{
		[JsonPropertyName("id")]
		public Guid Id { get; set; }
		[JsonPropertyName("first_name")]
		public required string FirstName { get; set; }
		[JsonPropertyName("last_name")]
		public required string LastName { get; set; }
		[JsonPropertyName("profile_photo")]
		public required string ProfilePhoto { get; set; }
		[JsonPropertyName("email")]
		public required string Email { get; set; }
		[JsonIgnore]
		public required string PasswordHash { get; set; }
		[JsonPropertyName("role")]
		public Role? Role { get; set; }
		[JsonPropertyName("is_active")]
		public bool IsActive { get; set; }
		[JsonPropertyName("registration_date")]
		public DateOnly RegistrationDate { get; set; }
		[JsonPropertyName("orders")]
		public List<Order>? Orders { get; set; }
		[JsonPropertyName("wishlist")]
		public List<Product>? Wishlist { get; set; }
		[JsonPropertyName("reviews")]
		public List<Review>? Reviews { get; set; }
		[JsonPropertyName("review_reviews")]
		public List<ReviewReview>? ReviewReviews { get; set; }
	}
}
