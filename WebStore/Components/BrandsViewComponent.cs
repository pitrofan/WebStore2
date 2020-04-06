using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Components
{
    public class BrandsViewComponent : ViewComponent
    {
        private readonly IProductData productData;

        public BrandsViewComponent(IProductData productData) => this.productData = productData;

        public IViewComponentResult Invoke() => View(GetBrans());

        public IEnumerable<BrandViewModel> GetBrans() => productData
            .GetBrands()
            .Select(x => new BrandViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Order = x.Order
            })
            .OrderBy(x => x.Order);
    }
}
