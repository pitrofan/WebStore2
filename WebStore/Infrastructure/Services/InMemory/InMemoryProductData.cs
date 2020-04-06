using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Services.InMemory
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Brand> GetBrands() => TestData.Brands;

        public Product GetProductById(int id) => TestData.Products.FirstOrDefault(x => x.Id == id);

        public IEnumerable<Product> GetProducts(ProductFilter Filter = null)
        {
            var query = TestData.Products;

            if (Filter?.SectionId != null)
                query = query.Where(x => x.SectionId == Filter.SectionId);

            if (Filter?.BrandId != null)
                query = query.Where(x => x.BrandId == Filter.BrandId);

            return query;
        }


        public IEnumerable<Section> GetSections() => TestData.Sections;
    }
}
