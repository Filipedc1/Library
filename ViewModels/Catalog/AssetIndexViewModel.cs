using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.ViewModels.Catalog
{
    public class AssetIndexViewModel
    {
        public IEnumerable<AssetIndexListingViewModel> Assets { get; set; }
    }
}
