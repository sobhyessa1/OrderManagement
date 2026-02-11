using Ardalis.Result;
using MediatR;
using OrderManagement.Application.Features.Orders.Commands.CreateOrder;

namespace OrderManagement.Application.Features.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersQuery:IRequest<Result<List<CreateOrderDTO>>>
    {

    }
}
