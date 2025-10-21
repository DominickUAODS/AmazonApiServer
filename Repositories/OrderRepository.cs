using AmazonApiServer.Data;
using AmazonApiServer.DTOs.DeliveryAddress;
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
				.Include(o => o.DeliveryAddress)
					.ThenInclude(a => a.Country)
				.Include(o => o.DeliveryAddress)
					.ThenInclude(a => a.State)
				.Select(o => new OrderDto
				{
					Id = o.Id,
					UserId = o.UserId,
					RecipientFirstName = o.RecipientFirstName,
					RecipientLastName = o.RecipientLastName,
					RecipientEmail = o.RecipientEmail,
					Address = new DeliveryAddressDto
					{
						Id = o.DeliveryAddress.Id,
						CountryId = o.DeliveryAddress.CountryId,
						CountryName = o.DeliveryAddress.Country.Name,
						StateId = o.DeliveryAddress.StateId,
						StateName = o.DeliveryAddress.State != null ? o.DeliveryAddress.State.Name : null,
						City = o.DeliveryAddress.City,
						Postcode = o.DeliveryAddress.Postcode
					},
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
				.Include(o => o.OrderItems)
					.ThenInclude(i => i.Product)
						.ThenInclude(p => p.Displays)
				.Include(o => o.DeliveryAddress)
					.ThenInclude(a => a.Country)
				.Include(o => o.DeliveryAddress)
					.ThenInclude(a => a.State)
				.FirstOrDefaultAsync(o => o.Id == orderId);

			if (o == null) return null;

			return new OrderDto
			{
				Id = o.Id,
				UserId = o.UserId,
				RecipientFirstName = o.RecipientFirstName,
				RecipientLastName = o.RecipientLastName,
				RecipientEmail = o.RecipientEmail,
				DeliveryAddressId = o.DeliveryAddressId,
				Address = new DeliveryAddressDto
				{
					Id = o.DeliveryAddress.Id,
					CountryId = o.DeliveryAddress.CountryId,
					CountryName = o.DeliveryAddress.Country.Name,
					CountryStr = o.DeliveryAddress.CountryStr,
					StateId = o.DeliveryAddress.StateId,
					StateStr = o.DeliveryAddress.StateStr,
					StateName = o.DeliveryAddress.State != null ? o.DeliveryAddress.State.Name : null,
					City = o.DeliveryAddress.City,
					Postcode = o.DeliveryAddress.Postcode
				},
				PaymentType = o.PaymentType.ToString(),
				OrderStatus = o.Status.ToString(),
				OrderedOn = o.OrderedOn,
				Items = o.OrderItems.Select(i => new OrderItemDto
				{
					ProductId = i.ProductId,
					ProductName = i.Product?.Code ?? "",
					ProductImage = i.Product!.Displays.FirstOrDefault()?.Image ?? string.Empty,
					Number = i.Number
				}).ToList()
			};
		}

		public async Task<OrderDto?> CreateOrderAsync(OrderCreateDto dto)
		{
			var deliveryAddress = new DeliveryAddress
			{
				Id = Guid.NewGuid(),
				CountryId = dto.Address.CountryId,
				CountryStr = dto.Address.CountryStr,
				StateId = dto.Address.StateId,
				StateStr = dto.Address.StateStr,
				City = dto.Address.City,
				Postcode = dto.Address.Postcode
			};
			_context.DeliveryAddresses.Add(deliveryAddress);

			var order = new Order
			{
				Id = Guid.NewGuid(),
				UserId = dto.UserId,
				RecipientFirstName = dto.RecipientFirstName,
				RecipientLastName = dto.RecipientLastName,
				RecipientEmail = dto.RecipientEmail,
				DeliveryAddressId = deliveryAddress.Id,
				DeliveryAddress = deliveryAddress,
				PaymentType = dto.PaymentType,
				OrderedOn = DateTime.UtcNow,
				Status = OrderStatus.RECEIVED
			};

			_context.Orders.Add(order);
			await _context.SaveChangesAsync();

			var orderItems = dto.Items.Select(i => new OrderItem
			{
				Id = Guid.NewGuid(),
				OrderId = order.Id,
				ProductId = i.ProductId,
				Number = i.Number
			}).ToList();

			_context.OrderItems.AddRange(orderItems);
			await _context.SaveChangesAsync();

			return await GetOrderByIdAsync(order.Id);
		}

		public async Task<OrderDto?> UpdateOrderAsync(OrderUpdateDto dto)
		{
			var order = await _context.Orders
				.Include(o => o.OrderItems)
				.Include(o => o.DeliveryAddress)
				.FirstOrDefaultAsync(o => o.Id == dto.Id);

			if (order == null) return null;

			order.RecipientFirstName = dto.RecipientFirstName;
			order.RecipientLastName = dto.RecipientLastName;
			order.RecipientEmail = dto.RecipientEmail;
			order.PaymentType = dto.PaymentType;
			order.Status = dto.Status;

			if (order.DeliveryAddress != null && dto.Address != null)
			{
				order.DeliveryAddress.CountryId = dto.Address.CountryId;
				order.DeliveryAddress.CountryStr = dto.Address.CountryStr;
				order.DeliveryAddress.StateId = dto.Address.StateId;
				order.DeliveryAddress.StateStr = dto.Address.StateStr;
				order.DeliveryAddress.City = dto.Address.City;
				order.DeliveryAddress.Postcode = dto.Address.Postcode;
			}

			_context.OrderItems.RemoveRange(order.OrderItems);

			var newItems = dto.Items.Select(i => new OrderItem
			{
				Id = Guid.NewGuid(),
				OrderId = order.Id,
				ProductId = i.ProductId,
				Number = i.Number
			}).ToList();

			_context.OrderItems.AddRange(newItems);

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
						.ThenInclude(p => p.Displays)
				.Include(o => o.DeliveryAddress)
					.ThenInclude(a => a.Country)
				.Include(o => o.DeliveryAddress)
					.ThenInclude(a => a.State)
				.ToListAsync();

			return orders.Select(o => new OrderDto
			{
				Id = o.Id,
				//UserId = o.UserId,
				//RecipientFirstName = o.RecipientFirstName,
				//RecipientLastName = o.RecipientLastName,
				//RecipientEmail = o.RecipientEmail,
				//DeliveryAddressId = o.DeliveryAddressId,
				//Address = o.DeliveryAddress == null
				//	? null
				//	: new DeliveryAddressDto
				//	{
				//		Id = o.DeliveryAddress.Id,
				//		CountryId = o.DeliveryAddress.CountryId,
				//		CountryName = o.DeliveryAddress.Country.Name,
				//		StateId = o.DeliveryAddress.StateId,
				//		StateName = o.DeliveryAddress.State != null ? o.DeliveryAddress.State.Name : null,
				//		City = o.DeliveryAddress.City,
				//		Postcode = o.DeliveryAddress.Postcode
				//	},
				//PaymentType = o.PaymentType.ToString(),
				OrderStatus = o.Status.ToString(),
				OrderedOn = o.OrderedOn,
				ItemsCount = o.OrderItems.Count,
				TotalPrice = (decimal)o.OrderItems.Sum(i => (i.Product?.Price ?? 0) * i.Number)
				//.Select
				//(i => new OrderItemDto
				//{
				//	ProductId = i.ProductId,
				//	ProductName = i.Product?.Code ?? "",
				//	ProductImage = i.Product?.Displays.FirstOrDefault()?.Image ?? string.Empty,
				//	Number = i.Number
				//}).ToList()
			});
		}
	}
}