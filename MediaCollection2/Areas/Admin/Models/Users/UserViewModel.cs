using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Models.Users
{
    public class UserViewModel
    {
        public string id { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }
    }
}
