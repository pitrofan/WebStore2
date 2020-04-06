namespace WebStore.Domain.Entities.Base.Interfaces
{
    /// <summary>
    /// Именованая сущность
    /// </summary>
    public interface INamedEntity : IBaseEntity
    {
        /// <summary>
        /// Название
        /// </summary>
        string Name { get; set; }
    }
}
