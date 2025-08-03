using System.Text.Json.Serialization;

namespace AmazonApiServer.Models
{
    public class ReviewReview
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
		public Guid ReviewId { get; set; }
		[JsonPropertyName("review")]
        public Review? Review { get; set; }
		public Guid UserId { get; set; }
		[JsonPropertyName("user")]
        public User? User { get; set; }
        [JsonPropertyName("is_helpful")]
        public bool IsHelpful { get; set; }
    }
}
