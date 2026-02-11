using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.Features.Orders.Commands.CreateOrder;
using OrderManagement.Application.Features.Orders.Commands.DeleteOrder;
using OrderManagement.Application.Features.Orders.Commands.UpdateOrder;
using OrderManagement.Application.Features.Orders.Queries.GetAllOrders;
using OrderManagement.Application.Features.Orders.Queries.GetOrderById;

namespace OrderManagement.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var query= new GetAllOrdersQuery();
            var result = await _mediator.Send(query);
            if (!result.IsSuccess)
                return NotFound(result.Errors);

            return Ok(result);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetOrderByIdQuery(id);
            var result=await _mediator.Send(query);
            if (!result.IsSuccess)
                return NotFound(result.Errors);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateOrderRequest request)
        {
            var command = new CreateOrderCommand(request);
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return NotFound(result.Errors);

            return Ok(result.Value);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateOrderRequest request)
        {
            request.Id = id;

            var command = new UpdateOrderCommand(request);
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return NotFound(result.Errors);

            return Ok(result.Value);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteOrderCommand(id);
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return NotFound(result.Errors);

            return NoContent(); 
        }
    }
}
