using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.DTOs;
using OrderManagement.Application.Services;
using OrderManagement.Domain.Repositories;

namespace OrderManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var orders = _orderService.GetAll();
            return Ok(orders);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var order = _orderService.GetById(id);
            if (order == null)
                return NotFound("Order Not Found");
            return Ok(order);
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateOrderDto dto)
        {
            if (dto == null) return BadRequest("Order data is required");
            var order = _orderService.Create(dto);
            if (order == null)
                return BadRequest("Invalid Order Data");
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CreateOrderDto dto)
        {
            if (dto == null) return BadRequest("Order data is required");
            var order = _orderService.Update(id, dto);
            if (order == null)
                return NotFound("Order Not Found");
            return Ok(order);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _orderService.Delete(id);
            return NoContent();
        }
    }
}
