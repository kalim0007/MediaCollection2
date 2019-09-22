using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Domain
{
    public class Movie
    {
        public int ID { get; set; }
        public string Titel { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int GenreID { get; set; }
        public int DirectorID { get; set; }
        public int WriterID { get; set; }
        //ffddfdsf
        public string UserID { get; set; }
        public Director Director { get; set; }
        public Writer Writer { get; set; }
        public ICollection<Genre> Genres { get; set; }
        public ICollection<Review> Rviews { get; set; }

    }
}
