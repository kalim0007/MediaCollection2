using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Models.MusicModels.MusicPlaylist
{
    public class MusicPlaylistViewModel
    {
        public int ID { get; set; }
        public string Naam { get; set; }
        public string UserID { get; set; }
        public string User { get; set; }
        public bool Public { get; set; }
        public List<MusicViewModels> Musics { get; set; } = new List<MusicViewModels>();
    }
}
