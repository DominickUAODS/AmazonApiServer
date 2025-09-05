using AmazonApiServer.Data;
using AmazonApiServer.DTOs.Order;
using AmazonApiServer.DTOs.OrderItem;
using AmazonApiServer.Enums;
using AmazonApiServer.Interfaces;
using AmazonApiServer.Models;
using Microsoft.EntityFrameworkCore;

namespace AmazonApiServer.Repositories
{
	public class OrderRepository : IOrder
	{
		private readonly ApplicationContext _context;

		public OrderRepository(ApplicationContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
		{
			return await _context.Orders
				.Include(o => o.OrderItems)
				.ThenInclude(i => i.Product)
				.Select(o => new OrderDto
				{
					Id = o.Id,
					UserId = o.UserId,
					RecipientsName = o.RecipientsName,
					Address = o.Address,
					PaymentType = o.PaymentType.ToString(),
					OrderStatus = o.Status.ToString(),
					OrderedOn = o.OrderedOn,
					Items = o.OrderItems!.Select(i => new OrderItemDto
					{
						ProductId = i.ProductId,
						ProductName = i.Product!.Code,
						ProductImage = i.Product.Displays.FirstOrDefault().Image ?? string.Empty,
						Number = i.Number
					}).ToList()
				})
				.ToListAsync();
		}

		public async Task<OrderDto?> GetOrderByIdAsync(Guid orderId)
		{
			var o = await _context.Orders
				.Include(o => o.OrderItems)!
				.ThenInclude(i => i.Product)
				.FirstOrDefaultAsync(o => o.Id == orderId);

			if (o == null) return null;

			return new OrderDto
			{
				Id = o.Id,
				UserId = o.UserId,
				RecipientsName = o.RecipientsName,
				Address = o.Address,
				PaymentType = o.PaymentType.ToString(),
				OrderStatus = o.Status.ToString(),
				OrderedOn = o.OrderedOn,
				Items = o.OrderItems.Select(i => new OrderItemDto
				{
					ProductId = i.ProductId,
					ProductName = i.Product?.Code ?? "",
					ProductImage = i.Product!.Displays.FirstOrDefault().Image ?? string.Empty,
					Number = i.Number
				}).ToList()
			};
		}

		public async Task<OrderDto?> CreateOrderAsync(OrderCreateDto dto)
		{
			var order = new Order
			{
				Id = Guid.NewGuid(),
				UserId = dto.UserId,
				RecipientsName = dto.RecipientsName,
				Address = dto.Address,
				PaymentType = dto.PaymentType,
				OrderedOn = DateTime.UtcNow,
				Status = OrderStatus.RECEIVED,
				OrderItems = dto.Items.Select(i => new OrderItem
				{
					Id = Guid.NewGuid(),
					ProductId = i.ProductId,
					Number = i.Number
				}).ToList()
			};

			_context.Orders.Add(order);
			await _context.SaveChangesAsync();

			return await GetOrderByIdAsync(order.Id);
		}

		public async Task<OrderDto?> UpdateOrderAsync(OrderUpdateDto dto)
		{
			var order = await _context.Orders
				.Include(o => o.OrderItems)
				.FirstOrDefaultAsync(o => o.Id == dto.Id);

			if (order == null) return null;

			order.RecipientsName = dto.RecipientsName;
			order.Address = dto.Address;
			order.PaymentType = dto.PaymentType;
			order.Status = dto.Status;

			order.OrderItems = dto.Items.Select(i => new OrderItem
			{
				Id = Guid.NewGuid(),
				ProductId = i.ProductId,
				Number = i.Number
			}).ToList();

			await _context.SaveChangesAsync();
			return await GetOrderByIdAsync(order.Id);
		}

		public async Task<bool> DeleteOrderAsync(Guid id)
		{
			var order = await _context.Orders.FindAsync(id);
			if (order == null) return false;

			_context.Orders.Remove(order);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<IEnumerable<OrderDto?>> GetOrdersByUserIdAsync(Guid userId)
		{
			var orders = await _context.Orders
				.Where(o => o.UserId == userId)
				.Include(o => o.OrderItems)
				.ThenInclude(i => i.Product)
				.ToListAsync();

			return orders.Select(o => new OrderDto
			{
				Id = o.Id,
				UserId = o.UserId,
				RecipientsName = o.RecipientsName,
				Address = o.Address,
				PaymentType = o.PaymentType.ToString(),
				OrderStatus = o.Status.ToString(),
				OrderedOn = o.OrderedOn,
				Items = o.OrderItems.Select(i => new OrderItemDto
				{
					ProductId = i.ProductId,
					ProductName = i.Product?.Code ?? string.Empty,
					ProductImage = i.Product!.Displays.FirstOrDefault().Image ?? string.Empty,
					Number = i.Number
				}).ToList()
			});
		}
	}
}
