using System.Text.Json.Serialization;

namespace AmazonApiServer.DTOs.User
{
	public class UserWishlistDto
	{
		[JsonPropertyName("id")]
		public Guid Id { get; set; }
		[JsonPropertyName("title")]
		public string Name { get; set; } = string.Empty;

		[JsonPropertyName("image")]
		public string Image { get; set; } = string.Empty;
		
		[JsonPropertyName("reviews")]
		public int Reviews { get; set; }

		[JsonPropertyName("rating")]
		public int Rating { get; set; }
		
		[JsonPropertyName("price")]
		public decimal Price { get; set; }
		
		[JsonPropertyName("discount")]
		public int? Discount { get; set; }
		
		[JsonPropertyName("old_price")]
		public decimal? OldPrice { get; set; }
	}
}
