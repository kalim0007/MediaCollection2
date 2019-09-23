﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaCollection2.Models.Movies;

namespace MediaCollection2.Models.Review
{
    public class MovieReviewCreateViewModel
    {
        public int ID { get; set; }
        public int MovieID { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
