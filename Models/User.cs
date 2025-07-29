using System.Text.Json.Serialization;
using AmazonApiServer.Interfaces;

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
		public string? PasswordHash { get; set; }
		[JsonPropertyName("role_id")]
		public Guid RoleId { get; set; }
		[JsonPropertyName("role")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Role? Role { get; set; }
		[JsonPropertyName("is_active")]
		public bool IsActive { get; set; }
		[JsonPropertyName("registration_date")]
		public DateOnly RegistrationDate { get; set; }
		[JsonPropertyName("orders")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public List<Order>? Orders { get; set; }
		[JsonPropertyName("wishlist")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public List<Product>? Wishlist { get; set; }
		[JsonPropertyName("reviews")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public List<Review>? Reviews { get; set; }
		[JsonPropertyName("review_reviews")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public List<ReviewReview>? ReviewReviews { get; set; }
	}
}
