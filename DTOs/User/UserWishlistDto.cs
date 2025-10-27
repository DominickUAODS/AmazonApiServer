using System.Text.Json.Serialization;

namespace AmazonApiServer.DTOs.User
{
	public class UserWishlistDto
	{
		[JsonPropertyName("id")]
		public Guid Id { get; set; }
		[JsonPropertyName("name")]
		public string Name { get; set; } = string.Empty;

		[JsonPropertyName("display")]
		public string Image { get; set; } = string.Empty;
		
		[JsonPropertyName("comments")]
		public int Reviews { get; set; }

		[JsonPropertyName("stars")]
		public int Rating { get; set; }
		
		[JsonPropertyName("price")]
		public decimal Price { get; set; }
		
		[JsonPropertyName("discount")]
		public int? Discount { get; set; }
		
		[JsonPropertyName("old_cost")]
		public decimal? OldPrice { get; set; }
	}
}
