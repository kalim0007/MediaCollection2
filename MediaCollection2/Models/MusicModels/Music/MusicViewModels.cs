﻿using MediaCollection2.Models.MusicModels.MusicPlaylist;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Models.MusicModels
{
    public class MusicViewModels
    {
        public int ID { get; set; }
        public string Titel { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public int Lenght { get; set; }
        public int Rating { get; set; }
        public IFormFile Photo { get; set; }
        public bool WantToListen { get; set; }
        public bool Listened { get; set; }
        public string PhotoPath { get; set; }
        public string YoutubeTrailer { get; set; }
        public int playlist { get; set; }
        public List<SelectListItem> Playlist { get; set; }

    }
}
