using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Models.Genre
{
    public class MovieGenreDeleteViewModel
    {
        public int ID { get; set; }
        public int MovieID { get; set; }
        public int Rating { get; set; }
        public string Name { get; set; }
    }
}
