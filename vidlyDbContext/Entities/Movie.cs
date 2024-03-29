﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace vidlyDbContext.Entities
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public int Year { get; set; }
        [DisplayName("Upload Image")]
        public string ImagePath { get; set; }
        public int BorrowCount { get; set; }

        
    }
}
