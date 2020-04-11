using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles =Role.Administrator)]
    public class ProductsController : Controller
    {
        private readonly IProductData productData;

        public ProductsController(IProductData productData) => this.productData = productData;


        public IActionResult Index() => View(productData.GetProducts());

        public IActionResult Edit(int? id)
        {
            var product = id is null ? new Product() : productData.GetProductById((int)id);

            if (product is null)
                return NotFound();

            return View(product);
        }

        public IActionResult Delete(int id)
        {
            var product = productData.GetProductById((int)id);

            if (product is null)
                return NotFound();

            return View(product);
        }

        [HttpPost,ValidateAntiForgeryToken, ActionName(nameof(Delete))]
        public IActionResult DeleteConfirm(int id) => RedirectToAction(nameof(Index));
    }
}