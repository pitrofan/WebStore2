using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.DTO.Orders;
using WebStore.Domain.Entities.Orders;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.ServiceHosting.Controllers
{
    //[Route("api/[controller]")]
    [Route(WebApi.Orders)]
    [ApiController]
    public class OrdersApiController : ControllerBase, IOrderService
    {
        private readonly IOrderService orderService;

        public OrdersApiController(IOrderService orderService) => this.orderService = orderService;


        [HttpPost("{Username?}")]
        public Task<OrderDTO> CreateOrderAsync(string Username, CreateOrderModel orderModel) => orderService.CreateOrderAsync(Username, orderModel);

        [HttpGet("{id}")]
        public OrderDTO GetOrderById(int id) => orderService.GetOrderById(id);

        [HttpGet("user/{Username}")]
        public IEnumerable<OrderDTO> GetUserOrders(string Username) => orderService.GetUserOrders(Username);
    }
}