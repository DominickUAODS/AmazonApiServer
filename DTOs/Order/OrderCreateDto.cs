using AmazonApiServer.DTOs.DeliveryAddress;
using AmazonApiServer.DTOs.OrderItem;
using AmazonApiServer.Enums;
using AmazonApiServer.Models;

namespace AmazonApiServer.DTOs.Order
{
	public class OrderCreateDto
	{
		public Guid UserId { get; set; }
		public string RecipientFirstName { get; set; } = string.Empty;
		public string RecipientLastName { get; set; } = string.Empty;
		public string RecipientEmail { get; set; } = string.Empty;
		public DeliveryAddressCreateDto Address { get; set; } = null!;
		public PaymentType PaymentType { get; set; }
		public List<OrderItemCreateDto> Items { get; set; } = new();
	}
}
