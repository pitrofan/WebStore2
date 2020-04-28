using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.Domain.DTO.Products;

namespace WebStore.Infrastructure.Interfaces
{
    /// <summary>
    /// Каталог товаров
    /// </summary>
    public interface IProductData
    {
        /// <summary>
        /// Получить все секции
        /// </summary>
        /// <returns></returns>
        IEnumerable<Section> GetSections();

        SectionDTO GetSectionById(int id);

        /// <summary>
        /// Получить все бренды
        /// </summary>
        /// <returns></returns>
        IEnumerable<Brand> GetBrands();

        BrandDTO GetBrandById(int id);

        /// <summary>
        /// Товары из каталога
        /// </summary>
        /// <param name="Filters">Критерии поиска/фильтрации</param>
        /// <returns></returns>
        IEnumerable<ProductDTO> GetProducts(ProductFilter Filters = null);

        /// <summary>
        /// Получить товар по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ProductDTO GetProductById(int id);

    }
}
