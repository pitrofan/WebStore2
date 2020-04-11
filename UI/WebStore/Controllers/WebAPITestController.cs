using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces.Api;

namespace WebStore.Controllers
{
    public class WebAPITestController : Controller
    {
        private readonly IValuesService values;

        public WebAPITestController(IValuesService values) => this.values = values;


        public async Task<IActionResult> Index()
        {
            var valueses = await values.GetAsync();
            return View(valueses);
        }
    }
}