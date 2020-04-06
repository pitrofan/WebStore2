using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Services.InSQL
{
    public class SqlProductData : IProductData
    {
        private readonly WebStoreDB db;

        public SqlProductData(WebStoreDB _db) => db = _db;



        public IEnumerable<Brand> GetBrands() => db.Brands.Include(brand => brand.Products).AsEnumerable();

        public Product GetProductById(int id) => db.Products
            .Include(x => x.Brand)
            .Include(x => x.Section)
            .FirstOrDefault(x => x.Id == id);

        public IEnumerable<Product> GetProducts(ProductFilter Filters = null)
        {
            IQueryable<Product> query = db.Products;

            if (Filters?.BrandId != null)
                query = query.Where(product => product.BrandId == Filters.BrandId);

            if (Filters?.SectionId != null)
                query = query.Where(product => product.SectionId == Filters.SectionId);

            if(Filters?.Ids?.Count > 0)
                query = query.Where(product => Filters.Ids.Contains(product.Id));

            return query.AsEnumerable();
        }

        public IEnumerable<Section> GetSections() => db.Sections.Include(section => section.Products).AsEnumerable();
    }
}
