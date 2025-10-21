using AmazonApiServer.Enums;

namespace AmazonApiServer.DTOs.Order
{
	public class OrderUpdateDto : OrderCreateDto
	{
		public Guid Id { get; set; }
		public OrderStatus Status { get; set; }
	}
}
