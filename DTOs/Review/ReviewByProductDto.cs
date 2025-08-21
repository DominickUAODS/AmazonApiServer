using AmazonApiServer.Enums;
using System.Text.Json.Serialization;

namespace AmazonApiServer.DTOs.Review
{
    public class ReviewByProductDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("user_id")]
        public Guid UserId { get; set; }
        [JsonPropertyName("stars")]
        public int Stars { get; set; }
        [JsonPropertyName("title")]
        public required string Title { get; set; }
        [JsonPropertyName("content")]
        public required string Content { get; set; }
        [JsonPropertyName("published")]
        public DateTime Published { get; set; }
        [JsonPropertyName("helphul_count")]
        public int HelpfulCount { get; set; }
        [JsonPropertyName("is_helphul_for_you")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? IsHelpfulForYou { get; set; } // todo set for authorized users
        [JsonPropertyName("rewiew_tags")]
        public List<ProductReviewTag>? ReviewTags { get; set; }
        [JsonPropertyName("rewiew_images")]
        public List<string>? ReviewImages { get; set; }
    }
}
