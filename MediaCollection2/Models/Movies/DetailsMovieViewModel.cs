using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MediaCollection2.Models.Review;

namespace MediaCollection2.Models
{
    public class DetailsMovieViewModel
    {
        public int ID { get; set; }
        public string Titel { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public int Lenght { get; set; }
        public List<MovieReviewDetailsViewModel> Reviews { get; set; }
    }
}
