using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vidlyDbContext.Entities
{
    public abstract class UserBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public String Address { get; set; }
        public string UserType { get; set; }
    }
}
