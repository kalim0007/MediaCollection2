using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Models.Movies
{
    public class ListMovieViewModel
    {
        public int ID { get; set; }
        public string Titel { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public int Lenght { get; set; }
        public int Rating { get; set; }
        public string PhotoPath { get; set; }
        public int DirectorID { get; set; }
        public int WriterID { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string MoviePlaylistsID { get; set; }
        public bool WantToWatch { get; set; }
        public bool Watched { get; set; }
        public string Comment { get; set; }
        public string YoutubeTrailer { get; set; }
    }
}
