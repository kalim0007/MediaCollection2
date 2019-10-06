using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Domain.Series
{
    public class Season
    {
        public int ID { get; set; }
        public string Titel { get; set; }
        public int Nr { get; set; }
        public string PhotoPath { get; set; }
        public int SerieID { get; set; }
        public Serie Serie { get; set; }
        public ICollection<Episode> Episodes { get; set; }
    }
}
