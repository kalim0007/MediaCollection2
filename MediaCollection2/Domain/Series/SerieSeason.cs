using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Domain.Series
{
    public class SerieSeason
    {
        public int SeasonID { get; set; }
        public int SerieID { get; set; }
        public Serie Serie { get; set; }
        public Season Season { get; set; }
    }
}
