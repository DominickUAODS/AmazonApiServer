using System.Text.Json.Serialization;

namespace AmazonApiServer.DTOs.Product
{
	public class ProductInListDto
	{
		[JsonPropertyName("id")]
		public Guid Id { get; set; }
		[JsonPropertyName("name")]
		public required string Name { get; set; }
		[JsonPropertyName("price")]
		public float Price { get; set; }
		[JsonPropertyName("discount")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public int? Discount { get; set; }
		[JsonPropertyName("displays")]
		public required string Display { get; set; }
		[JsonPropertyName("stars")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public double? Rating { get; set; }
		[JsonPropertyName("comments")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public int? Comments { get; set; }
	}
}
