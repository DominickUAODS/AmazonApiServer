using System.Text.Json.Serialization;

namespace AmazonApiServer.Models
{
	public class ReviewReview
	{
		[JsonPropertyName("id")]
		public Guid Id { get; set; }
		
		[JsonPropertyName("review")]
		public Guid ReviewId { get; set; }

		[JsonIgnore]
		public Review? Review { get; set; }

		public Guid UserId { get; set; }

		[JsonPropertyName("user")]
		public User? User { get; set; }

		[JsonPropertyName("is_helpful")]
		public bool IsHelpful { get; set; }
	}
}
