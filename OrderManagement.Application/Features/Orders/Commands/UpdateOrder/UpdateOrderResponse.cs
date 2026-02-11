using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderResponse
    {
        public UpdateOrderDto order { get; set; }
    }

    public class UpdateOrderDto
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
