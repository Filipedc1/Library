using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class CheckoutViewModel
    {
        public string LibraryCardId { get; set; }
        public string Title { get; set; }
        public int AssetId { get; set; }
        public string ImageURL { get; set; }
        public int HoldCount { get; set; } //number of members that currently have this item on hold
        public bool IsCheckedOut { get; set; }
    }
}
