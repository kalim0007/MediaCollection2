using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Models.Review
{
    public class MovieReviewListViewModel
    {
        public int ID { get; set; }
        public int MovieID { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string Movie { get; set; }

    }
}
