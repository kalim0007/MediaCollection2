using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Models.Admin
{
    public class DetailRoleViewModel
    {
        public string id { get; set; }
        public string RoleName { get; set; }
        public List<string> Users { get; set; } = new List<string>();
    }
}
