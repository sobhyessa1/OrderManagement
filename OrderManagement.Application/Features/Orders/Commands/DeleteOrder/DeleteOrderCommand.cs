using Ardalis.Result;
using MediatR;

namespace OrderManagement.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public DeleteOrderCommand(int id)
        {
            Id= id;
        }
    }
}
