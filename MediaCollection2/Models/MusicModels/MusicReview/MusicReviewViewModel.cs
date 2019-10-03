using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Models.MusicModels
{
    public class MusicReviewViewModel
    {
        public int ID { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string Music { get; set; }
        public int MusicID { get; set; }
    }
}
