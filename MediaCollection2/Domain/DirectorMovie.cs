using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Domain
{
    public class DirectorMovie
    {
        public int MovieID { get; set; }
        public int DirectorID { get; set; }
        public Director Director { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
