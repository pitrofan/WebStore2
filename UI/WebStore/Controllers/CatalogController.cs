using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Mapping;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData productData;

        public CatalogController(IProductData productData) => this.productData = productData;


        public IActionResult Shop(int? SectionId, int? BrandId)
        {
            var products = productData.GetProducts(new ProductFilter
            {
                SectionId = SectionId,
                BrandId = BrandId
            });

            return View(new CatalogViewModel 
            {
                SectionId = SectionId,
                BrandId = BrandId,
                Products = products
                            .Select(ProductMapping.FromDTO)
                            .Select(ProductMapping.ToView)
                            .OrderBy(x => x.Order)
            });
        }

        public IActionResult Details(int id)
        {
            var product = productData.GetProductById(id);

            if (product is null)
                return NotFound();

            return View(product.FromDTO().ToView());
        }
    }
}
