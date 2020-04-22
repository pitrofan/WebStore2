using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
            //return View("OtherViewName");
        }


        public IActionResult SomeAction() => View();

        public IActionResult Throw(string id) => throw new ApplicationException(id);

        public IActionResult Error404() => View();

        public IActionResult Blog() => View();

        public IActionResult BlogSingle() => View();

        //public IActionResult Cart() => View(); // была корзина в Views -> Home -> Cart.cshtml

        //public IActionResult CheckOut() => View();

        public IActionResult ContactUs() => View();


        public IActionResult ErrorStatus(string Code) => RedirectToAction(nameof(Error404));
    }
}