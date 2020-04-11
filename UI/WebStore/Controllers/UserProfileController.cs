using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels.Orders;

namespace WebStore.Controllers
{
    public class UserProfileController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult Orders([FromServices] IOrderService orderService) => 
            View(orderService.GetUserOrders(User.Identity.Name)
                .Select(x => new UserOrderViewModel 
                {
                    Id = x.Id,
                    Name = x.Name,
                    Phone = x.Phone,
                    Address = x.Address,
                    TotalSum = x.OrderItems.Sum(i => i.Price * i.Quantity)
                }));



    }
}