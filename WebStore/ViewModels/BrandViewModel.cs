using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.ViewModels
{
    public class BrandViewModel : INamedEntity, IOrderedEntity
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public int Order { get; set; }
    }
}
