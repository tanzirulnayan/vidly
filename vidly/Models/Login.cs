using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vidly.Models
{
    public class Login
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
    }
}