using AmazonApiServer.Enums;
using System.Text.Json.Serialization;

namespace AmazonApiServer.Models
{
	public class ReviewTag
	{
		[JsonPropertyName("id")]
		public Guid Id { get; set; }

		[JsonPropertyName("review")]
		public Review? Review { get; set; }

		public Guid ReviewId { get; set; }

		[JsonPropertyName("tag")]
		public ProductReviewTag Tag { get; set; }
	}
}
