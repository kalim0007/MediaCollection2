using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Domain.music
{
    public class Music
    {
        public int ID { get; set; }
        public string Titel { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public int Lenght { get; set; }
        public string PhotoPath { get; set; }
        public bool WantToListen { get; set; }
        public bool Listened { get; set; }
        public ICollection<MusicDirector> Directors { get; set; }
        public ICollection<MusicWriter> Writers { get; set; }
        public ICollection<MusicGenre> Genres { get; set; }
        public ICollection<MusicReview> Reviews { get; set; }
    }
}
