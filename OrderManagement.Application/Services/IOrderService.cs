using OrderManagement.Application.DTOs;

namespace OrderManagement.Application.Services
{
    public interface IOrderService
    {
        List<OrderDto> GetAll();
        OrderDto GetById(int id);
        OrderDto Create(CreateOrderDto dto);
        OrderDto Update(int id, CreateOrderDto dto);
        void Delete(int id);
    }
}