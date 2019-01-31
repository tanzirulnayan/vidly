using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Razor;
using vidlyDbContext;
using vidlyDbContext.Entities;

namespace vidly.Models
{
    public class MovieViewModel
    {
        public Movie Movie { get; set; }
    }
}