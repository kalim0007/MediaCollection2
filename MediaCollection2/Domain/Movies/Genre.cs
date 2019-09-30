using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Domain
{
    public class Genre
    {
        public int ID { get; set; }
        public string Naam { get; set; }
        public int MovieID { get; set; }
        public Movie Movie { get; set; }
    }
}
