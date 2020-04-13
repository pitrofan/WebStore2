using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.DTO.Orders;
using WebStore.Domain.Entities.Orders;
using WebStore.ViewModels;
using WebStore.ViewModels.Orders;

namespace WebStore.Infrastructure.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<OrderDTO> GetUserOrders(string Username);

        OrderDTO GetOrderById(int id);

        Task<OrderDTO> CreateOrderAsync(string Username, CreateOrderModel orderModel);
    }
}
