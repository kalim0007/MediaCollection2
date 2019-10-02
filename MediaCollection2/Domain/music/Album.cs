using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Domain.music
{
    public class Album
    {
        public int ID { get; set; }
        public string Naam { get; set; }
        public ICollection<MusicPlaylistComb> Musics { get; set; }
    }
}
