using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Models.Movies
{
    public class CreateMovieViewModel
    {
        public int ID { get; set; }
        public string Titel { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public int Lenght { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public DateTime DirectorDateOfBirth { get; set; }
        public DateTime WriterDateOfBirth { get; set; }
    }
}
