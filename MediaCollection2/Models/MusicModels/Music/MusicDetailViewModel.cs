using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MediaCollection2.Models.MusicModels.MusicDirector;
using MediaCollection2.Models.MusicModels.MusicGenre;
using MediaCollection2.Models.MusicModels.MusicWriter;

namespace MediaCollection2.Models.MusicModels.Music
{
    public class MusicDetailViewModel
    {
        public int ID { get; set; }
        public string Titel { get; set; }
        public string Youtube { get; set; }
        public bool Listened { get; set; }
        public bool WantToListen { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public int Lenght { get; set; }
        public string PhotoPath { get; set; }
        public string Playlist { get; set; }
        public List<MusicReviewViewModel> Reviews { get; set; }
        public List<MusicGenreViewModel> Genres { get; set; }
        public List<MusicWriterViewModel> Writers { get; set; }
        public List<MusicDirectorViewModel> Directors { get; set; }
    }
}
