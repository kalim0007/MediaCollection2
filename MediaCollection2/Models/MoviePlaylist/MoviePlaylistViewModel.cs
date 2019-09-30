using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Models.MoviePlaylist
{
    public class MoviePlaylistViewModel
    {
        public int ID { get; set; }
        public string Naam { get; set; }
        public List<DeleteMovieViewModel> Movies { get; set; } = new List<DeleteMovieViewModel>();
    }
}
