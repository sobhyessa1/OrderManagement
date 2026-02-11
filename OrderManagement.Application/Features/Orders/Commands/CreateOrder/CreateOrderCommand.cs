using Ardalis.Result;
using MediatR;

namespace OrderManagement.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<Result<CreateOrderResponse>>
    {
        public CreateOrderRequest Request { get; set; }
        public CreateOrderCommand(CreateOrderRequest request)
        {
            Request = request;
        }



    }
    // manual implementation without MediatR
    //public class CreateOrderCommand
    //{
    //    public CreateOrderRequest Request { get; set; }
    //    public CreateOrderCommand(CreateOrderRequest request)
    //    {
    //        Request= request;
    //    }

    //}
}
