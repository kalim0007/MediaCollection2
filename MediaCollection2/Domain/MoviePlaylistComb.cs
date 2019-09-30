using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Domain
{
    public class MoviePlaylistComb
    {
        public int ID { get; set; }
        public int MovieID { get; set; }
        public int MoviePlaylistID { get; set; }
        public Movie Movie { get; set; }
        public MoviePlaylist MoviePlaylist { get; set; }
    }
}
