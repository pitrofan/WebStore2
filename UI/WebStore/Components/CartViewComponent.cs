using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Components
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ICartService cartService;

        public CartViewComponent(ICartService CartService) => cartService = CartService;

        public IViewComponentResult Invoke() => View(cartService.TransformFromCart());
    }
}
