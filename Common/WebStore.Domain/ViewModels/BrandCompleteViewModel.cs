using System;
using System.Collections.Generic;
using System.Text;
using WebStore.ViewModels;

namespace WebStore.Domain.ViewModels
{
    public class BrandCompleteViewModel
    {
        public IEnumerable<BrandViewModel> Brands { get; set; }

        public int? CurrentBrandId { get; set; }
    }
}
