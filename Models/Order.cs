using AmazonApiServer.Enums;
using System.Text.Json.Serialization;

namespace AmazonApiServer.Models
{
	public class Order
	{
		[JsonPropertyName("id")]
		public Guid Id { get; set; }

		public Guid UserId { get; set; }

		[JsonPropertyName("user")]
		public User? User { get; set; }

		[JsonPropertyName("order_status")]
		public OrderStatus Status { get; set; }

		[JsonPropertyName("ordered_on")]
		public DateTime OrderedOn { get; set; }

		[JsonPropertyName("recipient_first_name")]
		public required string RecipientFirstName { get; set; }

		[JsonPropertyName("recipient_last_name")]
		public required string RecipientLastName { get; set; }

		[JsonPropertyName("recipient_email")]
		public required string RecipientEmail { get; set; }

		public required DeliveryAddress DeliveryAddress { get; set; }
		[JsonPropertyName("address")]
		public required Guid DeliveryAddressId { get; set; }

		[JsonPropertyName("payment_type")]
		public PaymentType PaymentType { get; set; }

		[JsonPropertyName("order_items")]
		public List<OrderItem> OrderItems { get; set; } = [];
	}
}
