using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Domain
{
    public class Writer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int MovieID { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
