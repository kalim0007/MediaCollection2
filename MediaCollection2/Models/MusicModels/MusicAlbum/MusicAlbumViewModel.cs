using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Models.MusicModels.MusicAlbum
{
    public class MusicAlbumViewModel
    {
        public int ID { get; set; }
        public string Naam { get; set; }
        public string PhotoPath { get; set; }
        public IFormFile Photo { get; set; }
        public int Playlist { get; set; }
        public List<MusicViewModels> Musics { get; set; } = new List<MusicViewModels>();
    }
}
