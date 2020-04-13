using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.ServiceHosting.Controllers
{
    //[Route("api/[controller]")]
    [Route(WebApi.Products)]
    [ApiController]
    public class ProductsApiController : ControllerBase, IProductData
    {
        private readonly IProductData productData;

        public ProductsApiController(IProductData productData) => this.productData = productData;

        [HttpGet("brands")]
        public IEnumerable<Brand> GetBrands() => productData.GetBrands();

        [HttpGet("{id}")]
        public ProductDTO GetProductById(int id) => productData.GetProductById(id);

        [HttpPost]
        public IEnumerable<ProductDTO> GetProducts([FromBody] ProductFilter Filters = null) => productData.GetProducts(Filters);

        [HttpGet("sections")]
        public IEnumerable<Section> GetSections() => productData.GetSections();
    }
}