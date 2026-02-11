using FluentValidation;
using OrderManagement.Application.Features.Orders.Commands.UpdateOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Application.Validations.Orders
{
    public class UpdateOrderValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderValidator()
        {
            RuleFor(x => x.Request.Id)
                .GreaterThan(0)
                .WithMessage("Order ID must be greater than 0");

            RuleFor(x => x.Request.ProductName)
                .NotEmpty()
                .WithMessage("ProductName is required")
                .MaximumLength(100)
                .WithMessage("ProductName cannot exceed 100 characters");

            RuleFor(x => x.Request.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0");

            RuleFor(x => x.Request.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0");
        }
    }
}
