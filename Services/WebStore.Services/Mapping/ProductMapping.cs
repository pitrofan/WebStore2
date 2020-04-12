using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.DTO.Products;
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

        public static ProductDTO ToDTO(this Product product) => product is null ? null : new ProductDTO
        {
            Id = product.Id,
            Name = product.Name,
            Order = product.Order,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            Section = product.Section.ToDTO(),
            Brand = product.Brand.ToDTO()
        };

        public static Product FromDTO(this ProductDTO product) => product is null ? null : new Product
        {
            Id = product.Id,
            Name = product.Name,
            Order = product.Order,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            SectionId = product.Section.Id,
            Section = product.Section.FromDTO(),
            BrandId = product.Brand?.Id,
            Brand = product.Brand.FromDTO()
        };
    }
}
