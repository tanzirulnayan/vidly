using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vidly.ViewModels
{
    public class CustomerBorrowHistory
    {
        public string MovieName { get; set; }
        public DateTime DateOfBorrow { get; set; }
        public DateTime ReturnDateOfBorrow { get; set; }
        public string StatusOfBorrow { get; set; }
        public string MoviePosterPath { get; set; }
    }
}