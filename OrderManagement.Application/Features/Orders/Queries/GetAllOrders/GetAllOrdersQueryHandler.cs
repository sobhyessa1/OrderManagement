using Ardalis.Result;
using MediatR;
using OrderManagement.Application.Features.Orders.Commands.CreateOrder;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Repositories;

namespace OrderManagement.Application.Features.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, Result<List<CreateOrderDTO>>>
    {
        private readonly IGenericRepository<Order> _repository;
        public GetAllOrdersQueryHandler(IGenericRepository<Order> repository)
        {
            _repository = repository;
        }
        public Task<Result<List<CreateOrderDTO>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders=_repository.GetAll();

            var OrdersDto = new List<CreateOrderDTO>();
            foreach (var order in orders)
            {
                OrdersDto.Add(new CreateOrderDTO
                {
                    Id = order.Id,
                    UserID = order.UserId,
                    ProductName = order.ProductName,
                    Price = order.Price,
                    Quantity = order.Quantity,
                    OrderDate = order.OrderDate,
                });         
            }
            return Task.FromResult(Result.Success(OrdersDto));
        }
    }
}
