using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Models.Wrtiters
{
    public class MovieWriterCreateViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int MovieID { get; set; }
        public string Movie { get; set; }
    }
}
