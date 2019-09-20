using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Models.Movies
{
    public class CreateMovieViewModel
    {
        public string Titel { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
    }
}
