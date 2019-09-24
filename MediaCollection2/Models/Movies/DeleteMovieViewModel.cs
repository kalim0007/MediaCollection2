using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Models
{
    public class DeleteMovieViewModel
    {
        public int ID { get; set; }
        public string Titel { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public int Lenght { get; set; }
        public int DirectorID { get; set; }
        public int WriterID { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
    }
}
