using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Domain.music
{
    public class MusicPlaylistComb
    {
        public int MusicPlaylistID { get; set; }
        public int MusicID { get; set; }
        public Music Musics { get; set; }
        public MusicPlaylist MusicPlaylist { get; set; }
    }
}
