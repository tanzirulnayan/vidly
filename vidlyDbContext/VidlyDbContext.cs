using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vidlyDbContext.Entities;

namespace vidlyDbContext
{
    public class VidlyDbContext : DbContext
    {
        public VidlyDbContext(): base("VidlyDbContext")
        {
            Configuration.ProxyCreationEnabled = false;
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Moderator> Moderators { get; set; }
        public DbSet<BorrowHistory> BorrowHistories { get; set; }

        //public System.Data.Entity.DbSet<vidly.Models.Login> Logins { get; set; }
    }
}
