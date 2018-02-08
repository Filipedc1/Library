using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData.Models
{
    public class LibraryCard
    {
        public int Id { get; set; }

        //overdue fees
        public decimal Fees { get; set; }

        public DateTime Created { get; set; }

        //how many books a card/member has currently in their possesion
        public virtual IEnumerable<Checkout> Checkouts { get; set; }
    }
}
