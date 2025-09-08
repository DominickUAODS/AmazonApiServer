using AmazonApiServer.DTOs.Order;

namespace AmazonApiServer.Interfaces
{
	public interface IOrder
	{
		Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
		Task<OrderDto?> GetOrderByIdAsync(Guid orderId);
		Task<IEnumerable<OrderDto?>> GetOrdersByUserIdAsync(Guid userId);
		Task<OrderDto?> CreateOrderAsync(OrderCreateDto dto);
		Task<OrderDto?> UpdateOrderAsync(OrderUpdateDto dto);
		Task<bool> DeleteOrderAsync(Guid id);
	}
}