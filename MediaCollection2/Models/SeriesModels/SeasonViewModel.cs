using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Models.SeriesModels
{
    public class SeasonViewModel
    {
        public int ID { get; set; }
        public int Nr { get; set; }
        public int Rating { get; set; }
        public IFormFile Photo { get; set; }
        public string PhotoPath { get; set; }
        public string Serie { get; set; }
        public string Titel { get; set; }
        public int SerieID { get; set; }
    }
}
