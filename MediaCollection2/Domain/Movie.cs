using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Domain
{
    public class Movie
    {
        public int ID { get; set; }
        public string Titel { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public int Lenght { get; set; }
        public string PhotoPath { get; set; }
        public string UserId { get; set; }
        public int MoviePlaylistID { get; set; }
        public IdentityUser User { get; set; }
        public MoviePlaylist Playlists { get; set; }
        public ICollection<Director> Directors { get; set; }
        public ICollection<Writer> Writers { get; set; }
        public ICollection<Genre> Genres { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
