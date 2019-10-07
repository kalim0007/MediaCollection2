using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Models.SeriesModels
{
    public class SerieViewModel
    {
        public int ID { get; set; }
        public string Titel { get; set; }
        public int Rating { get; set; }
        public string PhotoPath { get; set; }
        public string YoutubeTrailer { get; set; }
        public int SeasonID { get; set; }
        public int SeasonNr { get; set; }
    }
}
