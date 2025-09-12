using AmazonApiServer.DTOs.DeliveryAddress;
using AmazonApiServer.DTOs.OrderItem;

namespace AmazonApiServer.DTOs.Order
{

	public class OrderDto
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public string RecipientFirstName { get; set; } = string.Empty;
		public string RecipientLastName { get; set; } = string.Empty;
		public string RecipientEmail { get; set; } = string.Empty;
		public Guid DeliveryAddressId { get; set; }
		public DeliveryAddressDto Address { get; set; } = null!;
		public string PaymentType { get; set; } = string.Empty;
		public string OrderStatus { get; set; } = string.Empty;
		public DateTime OrderedOn { get; set; }
		public List<OrderItemDto> Items { get; set; } = [];
		public int ItemsCount { get; set; }
		public decimal TotalPrice { get; set; }
	}
}
