﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MediaCollection2.Models.Review;
using MediaCollection2.Models.Genre;
using MediaCollection2.Models.Directors;
using MediaCollection2.Models.Wrtiters;

namespace MediaCollection2.Models
{
    public class DetailsMovieViewModel
    {
        public int ID { get; set; }
        public string Titel { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public int Lenght { get; set; }
        public string PhotoPath { get; set; }
        public bool WantToWatch { get; set; }
        public bool Watched { get; set; }
        public string Playlist { get; set; }
        public string Youtube { get; set; }
        public string User { get; set; }
        public List<MovieReviewDetailsViewModel> Reviews { get; set; }
        public List<MovieGenreCreateViewModel> Genres { get; set; }
        public List<MovieWriterCreateViewModel> Writers { get; set; }
        public List<MovieDirectorCreateViewModel> Directors { get; set; }

    }
}
