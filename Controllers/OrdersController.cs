using AmazonApiServer.DTOs.Order;
using AmazonApiServer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmazonApiServer.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class OrdersController : ControllerBase
	{
		private readonly IOrder _orders;

		public OrdersController(IOrder orders)
		{
			_orders = orders;
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> GetAll()
		{
			var result = await _orders.GetAllOrdersAsync();
			return Ok(result);
		}

		[HttpGet("{id}")]
		[Authorize]
		public async Task<IActionResult> GetById(Guid id)
		{
			var result = await _orders.GetOrderByIdAsync(id);
			return result == null ? NotFound() : Ok(result);
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Create(OrderCreateDto dto)
		{
			var result = await _orders.CreateOrderAsync(dto);
			return result == null ? StatusCode(500) : CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
		}

		[HttpPut]
		[Authorize]
		public async Task<IActionResult> Update(OrderUpdateDto dto)
		{
			var result = await _orders.UpdateOrderAsync(dto);
			return result == null ? NotFound() : Ok(result);
		}

		[HttpDelete("{id}")]
		[Authorize]
		public async Task<IActionResult> Delete(Guid id)
		{
			var success = await _orders.DeleteOrderAsync(id);
			return success ? Ok() : NotFound();
		}
	}
}