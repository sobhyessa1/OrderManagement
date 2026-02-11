namespace OrderManagement.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderRequest
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
