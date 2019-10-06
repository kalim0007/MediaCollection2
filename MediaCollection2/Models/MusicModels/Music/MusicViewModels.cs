﻿using Microsoft.AspNetCore.Http;
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
        public IFormFile Photo { get; set; }
        public bool WantToListen { get; set; }
        public bool Listened { get; set; }
        public string PhotoPath { get; set; }
        public int playlist { get; set; }
    }
}