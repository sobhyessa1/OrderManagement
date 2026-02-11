using OrderManagement.Application.DTOs;
using OrderManagement.Application.Interfaces;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Repositories;

namespace OrderManagement.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IUserClient _userGateway;

        public OrderService(IGenericRepository<Order> orderRepository, IUserClient userGateway)
        {
            _orderRepository = orderRepository;
            _userGateway = userGateway;
        }

        public List<OrderDto> GetAll()
        {
            var orders = _orderRepository.GetAll();
            var orderDtos = new List<OrderDto>();

            foreach (var order in orders)
            {
                var dto = new OrderDto
                {
                    Id = order.Id,
                    UserId = order.UserId,
                    ProductName = order.ProductName,
                    Price = order.Price,
                    Quantity = order.Quantity,
                    OrderDate = order.OrderDate
                };
                orderDtos.Add(dto);
            }
            return orderDtos;
        }

        public OrderDto GetById(int id)
        {
            var order = _orderRepository.GetById(id);
            if (order == null)
                return null;
            return new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                ProductName = order.ProductName,
                Price = order.Price,
                Quantity = order.Quantity,
                OrderDate = order.OrderDate
            };
        }

        public OrderDto Create(CreateOrderDto dto)
        {
            bool userExists = _userGateway.IsUserExists(dto.UserId);

            if (!userExists)
            {
                throw new Exception($"User with ID {dto.UserId} not found. Cannot create order.");
            }

            if (dto == null)
                return null;
            var order = new Order
            {
                UserId = dto.UserId,
                ProductName = dto.ProductName,
                Price = dto.Price,
                Quantity = dto.Quantity,
                OrderDate = DateTime.Now
            };
            var createdOrder = _orderRepository.Add(order);
            return new OrderDto
            {
                Id = createdOrder.Id,
                UserId = createdOrder.UserId,
                ProductName = createdOrder.ProductName,
                Price = createdOrder.Price,
                Quantity = createdOrder.Quantity,
                OrderDate = createdOrder.OrderDate
            };
        }

        public OrderDto Update(int id, CreateOrderDto dto)
        {
            var existingOrder = _orderRepository.GetById(id);
            if (existingOrder == null)
                return null;

            existingOrder.ProductName = dto.ProductName;
            existingOrder.Price = dto.Price;
            existingOrder.Quantity = dto.Quantity;
            _orderRepository.Update(existingOrder);
            return new OrderDto
            {
                Id = existingOrder.Id,
                UserId = existingOrder.UserId,
                ProductName = existingOrder.ProductName,
                Price = existingOrder.Price,
                Quantity = existingOrder.Quantity,
                OrderDate = existingOrder.OrderDate
            };
        }

        public void Delete(int id)
        {
            var order = _orderRepository.GetById(id);
            if (order == null)
                return;
            _orderRepository.Delete(id);
        }
    }
}
