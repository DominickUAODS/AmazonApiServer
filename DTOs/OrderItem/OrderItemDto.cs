namespace AmazonApiServer.DTOs.OrderItem
{
	public class OrderItemDto
	{
		public Guid ProductId { get; set; }
		public string ProductName { get; set; } = string.Empty;
		public int Number { get; set; }
	}
}
