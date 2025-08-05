using AmazonApiServer.Data;
using AmazonApiServer.DTOs;
using AmazonApiServer.DTOs.Order;
using AmazonApiServer.DTOs.OrderItem;
using AmazonApiServer.DTOs.User;
using AmazonApiServer.Enums;
using AmazonApiServer.Interfaces;
using AmazonApiServer.Models;
using Microsoft.EntityFrameworkCore;

namespace AmazonApiServer.Repositories
{
	public class OrderService : IOrder
	{
		private readonly ApplicationContext _context;

		public OrderService(ApplicationContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<OrderDto>> GetAllAsync()
		{
			return await _context.Orders?
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
						Number = i.Number
					}).ToList()
				})
				.ToListAsync();
		}

		public async Task<OrderDto?> GetByIdAsync(Guid id)
		{
			var o = await _context.Orders
				.Include(o => o.OrderItems)!
				.ThenInclude(i => i.Product)
				.FirstOrDefaultAsync(o => o.Id == id);

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
				Items = o.OrderItems!.Select(i => new OrderItemDto
				{
					ProductId = i.ProductId,
					ProductName = i.Product?.Code ?? "",
					Number = i.Number
				}).ToList()
			};
		}

		public async Task<OrderDto?> CreateAsync(OrderCreateDto dto)
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

			return await GetByIdAsync(order.Id);
		}

		public async Task<OrderDto?> UpdateAsync(OrderUpdateDto dto)
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
			return await GetByIdAsync(order.Id);
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var order = await _context.Orders.FindAsync(id);
			if (order == null) return false;

			_context.Orders.Remove(order);
			await _context.SaveChangesAsync();
			return true;
		}
	}

}
