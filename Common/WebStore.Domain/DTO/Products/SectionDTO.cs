using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.DTO.Products
{
    public class SectionDTO : INamedEntity
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
