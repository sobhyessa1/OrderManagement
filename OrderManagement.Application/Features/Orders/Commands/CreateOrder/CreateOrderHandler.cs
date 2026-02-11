using Ardalis.Result;
using MediatR;
using OrderManagement.Application.Interfaces;
using OrderManagement.Application.Validations.Orders;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Repositories;


namespace OrderManagement.Application.Features.Orders.Commands.CreateOrder
{

    #region Without MediatR
    //public class CreateOrderHandler
    //{
    //    private readonly IGenericRepository<Order> _repository;
    //    private readonly IUserClient _userClient;

    //    public CreateOrderHandler(IGenericRepository<Order> repository, IUserClient userClient)
    //    {
    //        _repository = repository;
    //        _userClient = userClient;
    //    }

    //    public Result<CreateOrderResponse> Handle (CreateOrderCommand command)
    //    {
    //        var userExists = _userClient.IsUserExists(command.Request.UserID);
    //        if(!userExists)
    //            return Result.NotFound("User not found");

    //        var order = new Order
    //        {
    //            UserId = command.Request.UserID,
    //            ProductName = command.Request.ProductName,
    //            Quantity = command.Request.Quantity,
    //            Price = command.Request.Price,
    //            OrderDate = DateTime.Now
    //        };
    //        var createdOrder= _repository.Add(order);

    //        var response = new CreateOrderResponse
    //        {
    //            order = new CreateOrderDTO
    //            {
    //                UserID = createdOrder.UserId,
    //                ProductName = order.ProductName,
    //                Quantity = order.Quantity,
    //                Price = order.Price,
    //                OrderDate = order.OrderDate
    //            }
    //        };
    //        return Result.Success(response);
    //    }
    //}
    #endregion

    public class CreateOrderHandler:IRequestHandler<CreateOrderCommand, Result<CreateOrderResponse>>
    {
        private readonly IGenericRepository<Order> _repository;
        private readonly IUserClient _userClient;

        public CreateOrderHandler(IGenericRepository<Order> repository, IUserClient userClient)
        {
            _repository = repository;
            _userClient = userClient;
        }

        public async Task<Result<CreateOrderResponse>> Handle(CreateOrderCommand command,CancellationToken cancellationToken)
        {
            var validator=new CreateOrderValidator();
            var validationResult = validator.Validate(command);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
              .Select(e => new ValidationError(e.ErrorMessage))
              .ToList();
                return await Task.FromResult(Result<CreateOrderResponse>.Invalid(errors));
            }
            var userExists = _userClient.IsUserExists(command.Request.UserID);
            if (!userExists)
                return await Task.FromResult(Result.NotFound("User not found"));

            var order = new Order
            {
                UserId = command.Request.UserID,
                ProductName = command.Request.ProductName,
                Quantity = command.Request.Quantity,
                Price = command.Request.Price,
                OrderDate = DateTime.Now
            };
            var createdOrder = _repository.Add(order);

            var response = new CreateOrderResponse
            {
                order = new CreateOrderDTO
                {
                    UserID = createdOrder.UserId,
                    ProductName = order.ProductName,
                    Quantity = order.Quantity,
                    Price = order.Price,
                    OrderDate = order.OrderDate
                }
            };
            return await Task.FromResult(Result.Success(response));
        }
    }
}
