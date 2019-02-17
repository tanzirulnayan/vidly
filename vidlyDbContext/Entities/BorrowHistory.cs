using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vidlyDbContext.Entities
{
    public class BorrowHistory
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public int MovieId { get; set; }
        [ForeignKey("MovieId")]
        public Movie Movie { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        [Required]
        public DateTime BorrowDate { get; set; }
        [Required]
        public DateTime ReturnDate { get; set; }
        [Required]
        public string BorrowStatus { get; set; }
    }
}
