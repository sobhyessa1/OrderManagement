using FluentValidation;
using OrderManagement.Application.Features.Orders.Commands.CreateOrder;

namespace OrderManagement.Application.Validations.Orders
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            RuleFor(o => o.Request.UserID).GreaterThan(0).WithMessage("UserID must be greater than 0");
            RuleFor(o => o.Request.ProductName).NotEmpty().WithMessage("ProductName is required")
                                             .MaximumLength(100).WithMessage("ProductName cannot exceed 100 characters");
            RuleFor(x => x.Request.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0");

            RuleFor(x => x.Request.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0");

        }
    }
}
