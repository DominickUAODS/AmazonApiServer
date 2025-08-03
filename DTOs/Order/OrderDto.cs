using AmazonApiServer.DTOs.OrderItem;

namespace AmazonApiServer.DTOs.Order
{

	public class OrderDto
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public string RecipientsName { get; set; } = string.Empty;
		public string Address { get; set; } = string.Empty;
		public string PaymentType { get; set; } = string.Empty;
		public string OrderStatus { get; set; } = string.Empty;
		public DateOnly OrderedOn { get; set; }
		public List<OrderItemDto> Items { get; set; } = new();
	}
}
