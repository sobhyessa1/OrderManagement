using Ardalis.Result;
using MediatR;
using OrderManagement.Application.Features.Orders.Commands.CreateOrder;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Repositories;

namespace OrderManagement.Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Result<CreateOrderDTO>>
    {
        private readonly IGenericRepository<Order> _repo;

        public GetOrderByIdQueryHandler(IGenericRepository<Order> repo)
        {
            _repo = repo;
        }

        public Task<Result<CreateOrderDTO>> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
        {
            var order = _repo.GetById(query.Id);
            if (order == null)
                return Task.FromResult(Result<CreateOrderDTO>.NotFound("Order not found"));

            var dto = new CreateOrderDTO
            {
                Id = order.Id,
                UserID = order.UserId,
                ProductName = order.ProductName,
                Price = order.Price,
                Quantity = order.Quantity,
                OrderDate = order.OrderDate
            };

            return Task.FromResult(Result.Success(dto));
        }
    }
}
