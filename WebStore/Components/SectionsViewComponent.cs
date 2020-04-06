using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Components
{
    //[ViewComponent(Name ="CatalogSections")]
    public class SectionsViewComponent : ViewComponent
    {
        private readonly IProductData productData;

        public SectionsViewComponent(IProductData productData) => this.productData = productData;


        public IViewComponentResult Invoke() => View(GetSections());
        
        
        //public async Task<IViewComponentResult> InvokeAsync()
        //{
        //}


        private IEnumerable<SectionViewModel> GetSections()
        {
            var sections = productData.GetSections().ToArray();

            var parentSections = sections.Where(x => x.ParentId is null);

            var parentSectionsViews = parentSections
                .Select(x => new SectionViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Order = x.Order
                })
                .ToList();

            foreach(var parentSection in parentSectionsViews)
            {
                var Childs = sections.Where(x => x.ParentId == parentSection.Id);

                foreach (var ChildSection in Childs)
                    parentSection.ChildSections.Add(new SectionViewModel
                    {
                        Id = ChildSection.Id,
                        Name = ChildSection.Name,
                        Order = ChildSection.Order,
                        ParentSection = parentSection
                    });

                parentSection.ChildSections.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));
            }

            parentSectionsViews.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));

            return parentSectionsViews;
        }
    }
}
