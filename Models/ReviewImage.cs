using System.Text.Json.Serialization;

namespace AmazonApiServer.Models
{
    public class ReviewImage
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

		public Review? Review { get; set; }

		[JsonPropertyName("review")]
        public Guid ReviewId { get; set; }
        
        [JsonPropertyName("rewiew_image")]
        public required string Image { get; set; }
	}
}
