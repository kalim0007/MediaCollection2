﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Domain.music
{
    public class MusicGenre
    {
        public int ID { get; set; }
        public string Naam { get; set; }
        public int MusicID { get; set; }
        public Music Musics { get; set; }
    }
}
