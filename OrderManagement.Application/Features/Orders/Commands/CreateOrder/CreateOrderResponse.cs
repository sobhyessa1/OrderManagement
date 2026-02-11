

namespace OrderManagement.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderResponse
    {
        public CreateOrderDTO order { get; set; }
    }
    public class CreateOrderDTO
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public string ProductName { get; set; } 
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }

    }
}
