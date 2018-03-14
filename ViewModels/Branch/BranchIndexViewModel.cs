using System;
using System.Collections.Generic;

namespace Library.ViewModels.Branch
{
    public class BranchIndexViewModel
    {
        public IEnumerable<BranchDetailViewModel> Branches { get; set; }
    }
}
