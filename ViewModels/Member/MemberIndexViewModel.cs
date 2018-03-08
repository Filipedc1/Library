using Library.ViewModels.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.ViewModels.Member
{
    public class MemberIndexViewModel
    {
        public IEnumerable<MemberDetailViewModel> Members { get; set; }
    }
}
