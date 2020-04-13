using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WebStore.Clients.Base;
using WebStore.Domain;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Clients.Products
{
    public class ProductsClient : BaseClient, IProductData
    {
        public ProductsClient(IConfiguration configuration) : base(configuration, WebApi.Products)
        {
        }

        public IEnumerable<Brand> GetBrands() => Get<List<Brand>>($"{serviceAddress}/brands");

        public ProductDTO GetProductById(int id) => Get<ProductDTO>($"{serviceAddress}/{id}");

        public IEnumerable<ProductDTO> GetProducts(ProductFilter Filters = null) =>
            Post(serviceAddress, Filters ?? new ProductFilter())
            .Content
            .ReadAsAsync<List<ProductDTO>>()
            .Result;

        public IEnumerable<Section> GetSections() => Get<List<Section>>($"{serviceAddress}/sections");
    }
}
