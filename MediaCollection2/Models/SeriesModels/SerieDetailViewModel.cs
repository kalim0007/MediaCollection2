using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Models.SeriesModels
{
    public class SerieDetailViewModel
    {
        public int ID { get; set; }
        public string Titel { get; set; }
        public List<SeasonViewModel> Season { get; set; }
    }
}
