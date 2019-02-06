using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vidly.ViewModels
{
    public class ModeratorRentalHistory
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string MovieName { get; set; }
        public DateTime DateOfBorrow { get; set; }
        public DateTime ReturnDateOfBorrow { get; set; }
        public string StatusOfBorrow { get; set; }
    }
}