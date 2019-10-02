using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Domain.music
{
    public class MusicReview
    {
        public int ID { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int MusicsID { get; set; }
        public Music Musics { get; set; }
    }
}
