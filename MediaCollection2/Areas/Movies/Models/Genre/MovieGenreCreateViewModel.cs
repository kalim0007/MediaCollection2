using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Models.Genre
{
    public class MovieGenreCreateViewModel
    {
        public int ID { get; set; }
        public string Naam { get; set; }
        public int MovieID { get; set; }
        public string Movie { get; set; }
    }
}
