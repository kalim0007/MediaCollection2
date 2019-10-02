using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Domain.music
{
    public class MusicAlbum
    {
        public int AlbumID { get; set; }
        public int MusicID { get; set; }
        public Music Musics { get; set; }
        public Album Album { get; set; }
    }
}
