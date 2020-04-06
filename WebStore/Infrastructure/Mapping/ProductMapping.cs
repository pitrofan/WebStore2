using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Mapping
{
    public static class ProductMapping
    {
        public static ProductViewModel ToView(this Product product) => new ProductViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Order = product.Order,
            ImageUrl = product.ImageUrl,
            Price = product.Price,
            Brand = product.Brand?.Name
        };

        public static IEnumerable<ProductViewModel> ToView(this IEnumerable<Product> p) => p.Select(ToView);
    }
}
