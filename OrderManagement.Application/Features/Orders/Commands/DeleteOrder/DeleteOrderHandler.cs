using Ardalis.Result;
using MediatR;
using OrderManagement.Application.Validations.Orders;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Repositories;

namespace OrderManagement.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, Result>
    {
        private readonly IGenericRepository<Order> _repository;
        public DeleteOrderHandler(IGenericRepository<Order> repository)
        {
            _repository = repository;
        }

        public Task<Result> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            
            var order = _repository.GetById(command.Id);
            if (order == null)
                return Task.FromResult(Result.NotFound("Order not found"));
            _repository.Delete(command.Id);
            return Task.FromResult(Result.Success());
        }
    }
}
