using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.DTO.Products
{
    public class BrandDTO : INamedEntity
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
