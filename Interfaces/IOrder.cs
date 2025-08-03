using AmazonApiServer.DTOs.Order;

namespace AmazonApiServer.Interfaces
{
	public interface IOrder
	{
		Task<IEnumerable<OrderDto>> GetAllAsync();
		Task<OrderDto?> GetByIdAsync(Guid id);
		Task<OrderDto?> CreateAsync(OrderCreateDto dto);
		Task<OrderDto?> UpdateAsync(OrderUpdateDto dto);
		Task<bool> DeleteAsync(Guid id);
	}
}
