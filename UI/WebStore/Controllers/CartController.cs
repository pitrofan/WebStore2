using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.DTO.Orders;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;
using WebStore.ViewModels.Orders;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService) => this.cartService = cartService;


        public IActionResult Details() => View(new CartOrderViewModel 
        { 
            CartViewModel = cartService.TransformFromCart(), 
            OrderViewModel = new OrderViewModel() 
        });

        public IActionResult AddToCart(int id)
        {
            cartService.AddToCart(id);
            return RedirectToAction(nameof(Details));
        }

        public IActionResult DecrementFromCart(int id)
        {
            cartService.DecrementFromCart(id);
            return RedirectToAction(nameof(Details));
        }

        public IActionResult RemoveFromCart(int id)
        {
            cartService.RemoveFromCart(id);
            return RedirectToAction(nameof(Details));
        }

        public IActionResult RemoveAll()
        {
            cartService.RemoveAll();
            return RedirectToAction(nameof(Details));
        }

        public async Task<IActionResult> CheckOut(OrderViewModel model, [FromServices] IOrderService orderService)
        {
            if (!ModelState.IsValid)
                return View(nameof(Details), new CartOrderViewModel 
                {
                    CartViewModel = cartService.TransformFromCart(),
                    OrderViewModel = model
                });

            var orderModel = new CreateOrderModel
            {
                orderViewModel = model,
                orderItems = cartService.TransformFromCart()
                .Items
                .Select(x => new OrderItemDTO
                {
                    Id = x.Key.Id,
                    Price = x.Key.Price,
                    Quantity = x.Value
                })
                .ToList()
            };

            var order = await orderService.CreateOrderAsync(User.Identity.Name, orderModel);

            cartService.RemoveAll();

            return RedirectToAction(nameof(OrderConfirmed), new { id = order.Id });
        }

        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.OrderId = id; // ViewBag - динамич. объект. можно указывать любые свойства для передачи.

            return View();
        }


        #region API

        public IActionResult GetCartView() => ViewComponent("Cart");

        public IActionResult AddToCartAPI(int id)
        {
            cartService.AddToCart(id);
            return Json(new { id, message = $"Товар id:{id} был добавлен в корзину" });
        }

        public IActionResult DecrementFromCartAPI(int id)
        {
            cartService.DecrementFromCart(id);
            return Json(new { id, message = $"Количество товара с id:{id} было уменьшено на 1" });
        }

        public IActionResult RemoveFromCartAPI(int id)
        {
            cartService.RemoveFromCart(id);
            return Json(new { id, message = $"Товар id:{id} был удалён из корзины" });
        }

        public IActionResult RemoveAllAPI()
        {
            cartService.RemoveAll();
            return Json(new { message = "Корзина была успешно очищена" });
        }

        #endregion
    }
}