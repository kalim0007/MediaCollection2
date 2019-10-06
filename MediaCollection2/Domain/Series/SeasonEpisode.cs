using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Domain.Series
{
    public class SeasonEpisode
    {
        public int SeasonID { get; set; }
        public int EpisodesID { get; set; }
        public Season Season { get; set; }
        public Episode Episode { get; set; }

    }
}
