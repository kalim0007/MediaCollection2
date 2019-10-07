using MediaCollection2.Models.Movies;
using MediaCollection2.Models.MusicModels;
using MediaCollection2.Models.SeriesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Models.Home
{
    public class MyIndexViewModel
    {
        public List<ListMovieViewModel> Movies { get; set; }
        public List<MusicViewModels> Musics { get; set; }
        public List<SerieViewModel> Series { get; set; }
    }
}
