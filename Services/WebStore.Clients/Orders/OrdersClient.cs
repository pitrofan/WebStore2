using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebStore.Clients.Base;
using WebStore.Domain;
using WebStore.Domain.DTO.Orders;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Clients.Orders
{
    public class OrdersClient : BaseClient, IOrderService
    {
        public OrdersClient(IConfiguration configuration) : base(configuration, WebApi.Orders) { }


        public async Task<OrderDTO> CreateOrderAsync(string Username, CreateOrderModel orderModel)
        {
            var response = await PostAsync($"{serviceAddress}/{Username}", orderModel);
            return await response.Content.ReadAsAsync<OrderDTO>();
        }


        public OrderDTO GetOrderById(int id) => Get<OrderDTO>($"{serviceAddress}/{id}");

        public IEnumerable<OrderDTO> GetUserOrders(string Username) => Get<List<OrderDTO>>($"{serviceAddress}/user/{Username}");
    }
}
