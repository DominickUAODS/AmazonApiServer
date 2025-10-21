using System.Text.Json.Serialization;

namespace AmazonApiServer.Models
{
	public class DeliveryAddress
	{
		[JsonPropertyName("id")]
		public Guid Id { get; set; }
		public Country? Country { get; set; }
		public Guid? CountryId { get; set; }
		public string? CountryStr { get; set; }
		public State? State { get; set; }
		public Guid? StateId { get; set; }
		public string? StateStr { get; set; }
		public required string City { get; set; }
		public required string Postcode { get; set; }
	}
}
