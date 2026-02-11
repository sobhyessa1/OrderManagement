using Ardalis.Result;
using MediatR;
using OrderManagement.Application.Features.Orders.Commands.CreateOrder;
using OrderManagement.Application.Validations.Orders;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Repositories;

namespace OrderManagement.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, Result<UpdateOrderResponse>>
    {
        private readonly IGenericRepository<Order> _repository;

        public UpdateOrderHandler(IGenericRepository<Order> repository)
        {
            _repository = repository;
        }

        public  Task<Result<UpdateOrderResponse>> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateOrderValidator();
            var validationResult = validator.Validate(command);
            if(!validationResult.IsValid)
            {
                var errors = validationResult.Errors
             .Select(e => new ValidationError(e.ErrorMessage))
             .ToList();
                return  Task.FromResult(Result<UpdateOrderResponse>.Invalid(errors));
            }
            // 1. Get existing order
            var existingOrder = _repository.GetById(command.Request.Id);
            if (existingOrder == null)
                return Task.FromResult(Result<UpdateOrderResponse>.NotFound("Order not found"));

            // 2. Update the fields
            existingOrder.ProductName = command.Request.ProductName;
            existingOrder.Quantity = command.Request.Quantity;
            existingOrder.Price = command.Request.Price;

            var updatedOrder = _repository.Update(existingOrder);

            // 3. Map to response
            var response = new UpdateOrderResponse
            {
                order = new UpdateOrderDto
                {
                    Id = updatedOrder.Id,
                    UserID = updatedOrder.UserId,
                    ProductName = updatedOrder.ProductName,
                    Quantity = updatedOrder.Quantity,
                    Price = updatedOrder.Price,
                    OrderDate = updatedOrder.OrderDate
                }
            };

            return Task.FromResult(Result.Success(response));
        }
    }
}
