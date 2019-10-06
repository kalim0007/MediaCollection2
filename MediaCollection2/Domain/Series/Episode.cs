using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Domain.Series
{
    public class Episode
    {
        public int ID { get; set; }
        public string Titel { get; set; }
        public int EpisodeNr { get; set; }
        public int Length { get; set; }
        public int SeasonID { get; set; }
        public Season Season  { get; set; }
    }
}
