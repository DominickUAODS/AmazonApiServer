namespace AmazonApiServer.DTOs.OrderItem
{
	public class OrderItemDto
	{
		public Guid ProductId { get; set; }
		public string ProductName { get; set; } = string.Empty;
		public string ProductImage { get; set; } = string.Empty;
		public float ProductPrice { get; set; }
		public int Number { get; set; }
	}
}
