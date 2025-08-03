using AmazonApiServer.DTOs.Order;
using AmazonApiServer.Models;

namespace AmazonApiServer.Extensions
{
	public static class OrderExtensions
	{
		public static OrderDto ToDto(this Order order)
		{
			return new OrderDto
			{
				Id = order.Id,
				UserId = order.UserId,
				OrderedOn = order.OrderedOn,
				RecipientsName = order.RecipientsName,
				Address = order.Address,
				PaymentType = order.PaymentType.ToString(),
				OrderStatus = order.Status.ToString()
			};
		}

		public static Order ToEntity(this OrderCreateDto dto)
		{
			return new Order
			{
				Id = Guid.NewGuid(),
				UserId = dto.UserId,
				//OrderedOn = dto.OrderedOn,
				RecipientsName = dto.RecipientsName,
				Address = dto.Address,
				PaymentType = dto.PaymentType,
				//Status = dto.Status
			};
		}

		public static void UpdateFromDto(this Order order, OrderUpdateDto dto)
		{
			order.RecipientsName = dto.RecipientsName;
			order.Address = dto.Address;
			order.PaymentType = dto.PaymentType;
			order.Status = dto.Status;
			//order.OrderedOn = dto.OrderedOn;
		}
	}
}
