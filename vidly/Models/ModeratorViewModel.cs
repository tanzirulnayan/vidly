using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vidlyDbContext;
using vidlyDbContext.Entities;

namespace vidly.Models
{
    public class ModeratorViewModel : Moderator
    {
        public Moderator Moderator { get; set; }
    }
}