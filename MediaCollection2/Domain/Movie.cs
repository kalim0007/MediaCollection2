using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Domain
{
    public class Movie
    {
        public int ID { get; set; }
        public string Titel { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public int Lenght { get; set; }
        public int DirectorID { get; set; }
        public int WriterID { get; set; }
        public Director Director { get; set; }
        public Writer Writer { get; set; }
        public ICollection<Genre> Genres { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
