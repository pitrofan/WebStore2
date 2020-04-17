using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Mapping;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles =Role.Administrator)]
    public class ProductsController : Controller
    {
        private readonly IProductData productData;

        public ProductsController(IProductData productData) => this.productData = productData;


        public IActionResult Index([FromServices] IMapper mapper) => View(productData.GetProducts().Select(/*x => x.FromDTO()*/ mapper.Map<Product>));

        public IActionResult Edit(int? id, [FromServices] IMapper mapper)
        {
            //var product = id is null ? new Product() : productData.GetProductById((int)id).FromDTO();
            var product = id is null ? new Product() : mapper.Map<Product>(productData.GetProductById((int)id)); // Преобразование типов через автомаппер

            if (product is null)
                return NotFound();

            return View(product);
        }

        public IActionResult Delete(int id)
        {
            var product = productData.GetProductById((int)id);

            if (product is null)
                return NotFound();

            return View(product.FromDTO());
        }

        [HttpPost,ValidateAntiForgeryToken, ActionName(nameof(Delete))]
        public IActionResult DeleteConfirm(int id) => RedirectToAction(nameof(Index));
    }
}