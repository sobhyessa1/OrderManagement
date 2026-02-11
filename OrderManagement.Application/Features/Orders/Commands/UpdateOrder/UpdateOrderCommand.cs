using Ardalis.Result;
using MediatR;

namespace OrderManagement.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommand :IRequest<Result<UpdateOrderResponse>>
    {
        public UpdateOrderRequest Request { get; set; }
        public UpdateOrderCommand(UpdateOrderRequest request)
        {
            Request = request;
        }
    }
}
