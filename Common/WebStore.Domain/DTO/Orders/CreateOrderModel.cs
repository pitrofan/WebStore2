using System;
using System.Collections.Generic;
using System.Text;
using WebStore.ViewModels.Orders;

namespace WebStore.Domain.DTO.Orders
{
    public class CreateOrderModel
    {
        public OrderViewModel orderViewModel { get; set; }

        public List<OrderItemDTO> orderItems { get; set; }
    }
}
