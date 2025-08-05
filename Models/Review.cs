using System.Text.Json.Serialization;

namespace AmazonApiServer.Models
{
    public class Review
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        [JsonPropertyName("product")]
        public Product? Product { get; set; }
		public Guid UserId { get; set; }
		[JsonPropertyName("user")]
        public User? User { get; set; }
        [JsonPropertyName("stars")]
        public int Stars { get; set; }
        [JsonPropertyName("title")]
        public required string Title { get; set; }
        [JsonPropertyName("content")]
        public required string Content { get; set; }
        [JsonPropertyName("published")]
        public DateTime Published { get; set; }
        [JsonPropertyName("rewiew_reviews")]
        public List<ReviewReview>? ReviewReviews { get; set; }
        [JsonPropertyName("rewiew_tags")]
        public List<ReviewTag>? ReviewTags { get; set; }
        [JsonPropertyName("rewiew_images")]
        public List<ReviewImage>? ReviewImages { get; set; }
    }
}
