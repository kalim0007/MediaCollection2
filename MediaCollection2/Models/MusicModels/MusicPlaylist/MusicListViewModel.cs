using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Models.MusicModels.MusicPlaylist
{
    public class MusicListViewModel
    {
        public List<MusicViewModels> Musics { get; set; }
        public int Playlist { get; set; }
    }
}
