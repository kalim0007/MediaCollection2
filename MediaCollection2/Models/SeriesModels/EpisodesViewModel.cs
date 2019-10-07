using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Models.SeriesModels
{
    public class EpisodesViewModel
    {
        public int ID { get; set; }
        public string Titel { get; set; }
        public int Nr { get; set; }
        public int Length { get; set; }
        public int SeasonID { get; set; }
        public string Season { get; set; }
    }
}
