using System.Text.Json.Serialization;
using AmazonApiServer.DTOs.OrderItem;
using AmazonApiServer.Enums;

namespace AmazonApiServer.DTOs.Order
{
	public class OrderCreateDto
	{
		public Guid UserId { get; set; }
		public string RecipientsName { get; set; } = string.Empty;
		public string Address { get; set; } = string.Empty;
		public PaymentType PaymentType { get; set; }
		public List<OrderItemCreateDto> Items { get; set; } = new();
	}
}
