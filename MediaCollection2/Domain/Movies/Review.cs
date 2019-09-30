using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Domain
{
    public class Review
    {
        public int ID { get; set; }
        public int MovieID { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public Movie Movie { get; set; }
    }
}
