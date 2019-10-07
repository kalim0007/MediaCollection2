using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Domain
{
    public class MoviePlaylist
    {
        public int ID { get; set; }
        public bool Public { get; set; }
        public string Naam { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public ICollection<MoviePlaylistComb> Movies { get; set; }
    }
}
