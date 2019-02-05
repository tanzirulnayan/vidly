using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vidlyDbContext.Entities
{
    public class BorrowHistory
    {
        public Guid Id { get; set; } 
        public int MovieId { get; set; }
        public int CustomerId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string BorrowStatus { get; set; }
    }
}
