using AmazonApiServer.DTOs.DeliveryAddress;
using AmazonApiServer.DTOs.Order;
using AmazonApiServer.DTOs.OrderItem;
using AmazonApiServer.Enums;
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
				RecipientFirstName = order.RecipientFirstName,
				RecipientLastName = order.RecipientLastName,
				RecipientEmail = order.RecipientEmail,
				DeliveryAddressId = order.DeliveryAddressId,
				Address = order.DeliveryAddress != null
					? new DeliveryAddressDto
					{
						Id = order.DeliveryAddress.Id,
						CountryId = order.DeliveryAddress.CountryId,
						CountryName = order.DeliveryAddress.Country?.Name ?? "",
						StateId = order.DeliveryAddress.StateId,
						StateName = order.DeliveryAddress.State?.Name,
						City = order.DeliveryAddress.City,
						Postcode = order.DeliveryAddress.Postcode
					}
					: null,
				PaymentType = order.PaymentType.ToString(),
				OrderStatus = order.Status.ToString(),
				Items = order.OrderItems?.Select(i => new OrderItemDto
				{
					ProductId = i.ProductId,
					ProductName = i.Product?.Code ?? "",
					ProductImage = i.Product?.Displays.FirstOrDefault()?.Image ?? "",
					Number = i.Number
				}).ToList()
			};
		}

		public static Order ToEntity(this OrderCreateDto dto)
		{
			return new Order
			{
				Id = Guid.NewGuid(),
				UserId = dto.UserId,
				OrderedOn = DateTime.UtcNow,
				RecipientFirstName = dto.RecipientFirstName,
				RecipientLastName = dto.RecipientLastName,
				RecipientEmail = dto.RecipientEmail,
				DeliveryAddressId = dto.Address.Id,
				DeliveryAddress = new DeliveryAddress
				{
					Id = dto.Address.Id,
					CountryId = dto.Address.CountryId,
					StateId = dto.Address.StateId,
					City = dto.Address.City,
					Postcode = dto.Address.Postcode
				},
				PaymentType = dto.PaymentType,
				Status = OrderStatus.RECEIVED
			};
		}

		public static void UpdateFromDto(this Order order, OrderUpdateDto dto)
		{
			order.RecipientFirstName = dto.RecipientFirstName;
			order.RecipientLastName = dto.RecipientLastName;
			order.RecipientEmail = dto.RecipientEmail;
			order.PaymentType = dto.PaymentType;
			order.Status = dto.Status;

			if (order.DeliveryAddress != null && dto.Address != null)
			{
				order.DeliveryAddress.CountryId = dto.Address.CountryId;
				order.DeliveryAddress.StateId = dto.Address.StateId;
				order.DeliveryAddress.City = dto.Address.City;
				order.DeliveryAddress.Postcode = dto.Address.Postcode;
			}
		}
	}
}
